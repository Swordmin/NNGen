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
    }
    private void Update() 
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg - 90);
        _age += Time.deltaTime;
        _fitness = Vector2.Distance(transform.position, _target.position);
        UseBrain();
    }

    private void UseBrain()
    {
        float[] inputs = new float[2];
        inputs[0] = _target.position.x;
        inputs[1] = _target.position.y;
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
        _rigidbody.velocity = velocity;
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
        }
    }

}
