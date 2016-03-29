using UnityEngine;
using System.Collections.Generic;



public class MF_ProblemGenerator : MonoBehaviour
{

    public int maxProblemCount = 3; // 한 스테이지에서 나오는 문제 수
    public int minCommandNum = 5;
    public int maxCommandNum = 7;

    void Awake()
    {

    }

    public CommandType[] GetProblem()
    {
        List<CommandType> commandList = new List<CommandType>();

        int commandLength = Random.Range(minCommandNum, maxCommandNum - 1);

        for(int i = 0;i<commandLength;i++)
        {
            CommandType newCommand = (CommandType) Random.Range((int)CommandType.Left, (int)CommandType.Max);
            commandList.Add(newCommand);
        }

        return null;
    }
}