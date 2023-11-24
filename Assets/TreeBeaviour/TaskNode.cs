using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum TaskState { Running, Success, Failure }
public abstract class TaskBT 
{
    public abstract TaskState Execute();
}

public class TaskNode : Node
{
    protected List<TaskBT> Tasks { get; private set; } = new();
    private int CurrentTaskIndex { get; set; }

    public TaskNode(string tag, IEnumerable<TaskBT> tasks)
        :base(tag)
    {
        foreach (TaskBT task in tasks)
        {
            AddTask(task);
        }
    }

    public void AddTask(TaskBT task) => Tasks.Add(task);

    protected override NodeState InnerEvaluate()
    {
        bool executeNextTask = true;
        int taskCount = Tasks.Count;

        while (executeNextTask)
        {
            TaskBT currentTask = Tasks[CurrentTaskIndex];
            TaskState currentTaskState = currentTask.Execute();

            switch (currentTaskState)
            {
                case TaskState.Failure:
                    State = NodeState.Failure;
                    return State;
                case TaskState.Running:
                    executeNextTask = false;
                    break;
                case TaskState.Success:
                    if (CurrentTaskIndex == taskCount - 1)
                    {
                        CurrentTaskIndex = 0;
                        State = NodeState.Success;
                        return State;
                    }
                  
                    ++CurrentTaskIndex;
                    break;
            }
        }

        State = NodeState.Running;
        return State;
    }
}
public class TaskNodeRepeater : Node//meme chose que taskNode ,mais repete tout les action jusqu'a running ou failure a chaque InnerEvaluation
{
    protected List<TaskBT> Tasks { get; private set; } = new();

    public TaskNodeRepeater(string tag, IEnumerable<TaskBT> tasks)
        : base(tag)
    {
        foreach (TaskBT task in tasks)
        {
            AddTask(task);
        }
    }

    public void AddTask(TaskBT task) => Tasks.Add(task);

    protected override NodeState InnerEvaluate()
    {
        bool executeNextTask = true;
        int taskCount = Tasks.Count;
        int currentTaskIndex = 0;
        while (executeNextTask)
        {
            TaskBT currentTask = Tasks[currentTaskIndex];
            TaskState currentTaskState = currentTask.Execute();

            switch (currentTaskState)
            {
                case TaskState.Failure:
                    State = NodeState.Failure;
                    return State;
                case TaskState.Running:
                    executeNextTask = false;
                    break;
                case TaskState.Success:
                    if (currentTaskIndex == taskCount - 1)
                    {
                        currentTaskIndex = 0;
                        State = NodeState.Success;
                        return State;
                    }

                    ++currentTaskIndex;
                    break;
            }
        }

        State = NodeState.Running;
        return State;
    }
}
public class DummyTask : TaskBT
{
    private TaskState ReturnState { get; set; }
    private string Message { get; set; }
    public DummyTask(string message, TaskState returnState)
    {
        Message = message;
        ReturnState = returnState;
    }
    public override TaskState Execute()
    {
        Debug.Log(Message);
        return ReturnState;
    }
}

public class Wait : TaskBT
{
    private float ElapsedTime { get; set; } = 0;
    private float SecondsToWait { get; set; } = 0;

    public Wait(float secondsToWait) => SecondsToWait = secondsToWait;

    //On tient pour acquis qu'Execute va �tre appeler � chaque frame
    public override TaskState Execute()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime > SecondsToWait)
        {
            ElapsedTime = 0;
            return TaskState.Success;
        }

        return TaskState.Running;
    }
}

public class Patrol : TaskBT
{
    private Vector3[] Destinations { get; set; }
    private NavMeshAgent Agent { get; set; }
    private int CurrentDestinationID { get; set; }

    public Patrol(Vector3[] destinations, NavMeshAgent agent)
    {
        Destinations = destinations;
        Agent = agent;
    }

    public override TaskState Execute()
    {
        Vector3 currentDestination = Destinations[CurrentDestinationID];
        Agent.destination = currentDestination;

        if (Vector3.Distance(currentDestination, Agent.transform.position) < Agent.stoppingDistance)
        {
            CurrentDestinationID = (CurrentDestinationID +1) % Destinations.Length;
            return TaskState.Success;
        }
        return TaskState.Running;
    }
}
