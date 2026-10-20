using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLay;
    [SerializeField] private InputActionReference _mouseLeftInput;
    [SerializeField] private InputActionReference _mousePos;
    [SerializeField] private Transform _goalTransform;
    private Camera _cam;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _cam = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _mouseLeftInput.action.Enable();
        _mousePos.action.Enable();
        _mouseLeftInput.action.performed += OnMouseLeftButtonDown;
    }

    private void Start()
    {
        _goalTransform.position = transform.position;
    }

    private void Update()
    {
        MoveToGoal();
    }

    private void  OnMouseLeftButtonDown(InputAction.CallbackContext context)
    {
        Vector2 screen = _mousePos.action.ReadValue<Vector2>();
        Ray ray = _cam.ScreenPointToRay(screen);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _targetLay))
        {
            _goalTransform.position = hit.point;
        }
    }

    private void MoveToGoal()
    {
        //NavMesh 적용
        _agent.SetDestination(_goalTransform.position);

        //NavMesh 미적용
        //transform.position = Vector3.MoveTowards(transform.position, _goalTransform.position, 3 * Time.deltaTime);

    }
}