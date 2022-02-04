using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _age;
    [SerializeField] private float _health;
    [SerializeField] private Transform _target;
    [SerializeField] private float _fitness;
    [SerializeField] private Genome[] _genomes = new Genome[1] { new Genome()};
    [SerializeField] private bool _beginer;
    private NeuralNetwork _brain;

    private void SetBrain(NeuralNetwork brain) 
    {
        _brain = brain;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        int[] layers = new int[4] { 4, 8, 8, 1 };
        SetBrain(new NeuralNetwork(layers));
        if (_beginer) 
        {
            _genomes[0].value = UnityEngine.Random.Range(0,100f);
        }
    }
    private void Update() 
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg - 90);
        _age += Time.deltaTime;
       // _fitness = Vector2.Distance(transform.position, _target.position);
        UseBrain();
    }

    private void UseBrain()
    {
        if (_target == null)
        {
           _target = CellsManager.Manager.GetFood(transform, 10).transform;
        }
        float[] inputs = new float[3];
        inputs[0] = _target.position.x;
        inputs[1] = _target.position.y;
        inputs[2] = Convert.ToInt32(_target);
        float[] output = _brain.FeedForward(inputs);
        Vector2 target = Vector2.zero;
        Vector2 dir = (_target.position - transform.position);
        for (int i = 0; i < output.Length; i++)
        {
            target = dir * output[i];
        }
        if (target.magnitude > 1)
            target.Normalize();
        Vector2 velocity = _rigidbody.velocity;
        velocity += target;
        _rigidbody.velocity = velocity.normalized * 4;
    }


    private void FixedUpdate()
    {
    }

    private IEnumerator HungerTick() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1);
            _health--;
            if (_health <= 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Food food)) 
        {
            Destroy(collision.gameObject);
            _health += 10;
        }
    }

}
