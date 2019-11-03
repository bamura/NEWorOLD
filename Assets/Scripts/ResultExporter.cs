using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class ResultExporter : MonoBehaviour
{
    private string path;
    private string writeTxt;
    private List<string> writeTxtList;
    private string fileName = "result.txt";

    private int QuestionNumber;  //設問番号、何問目かを示す通し番号
    private int[] PictureNumberArray;      //ランダムな数字(PictureNumber)が被りなく順に並んでいる配列、indexはQuestionNumber-1を示す
    private int PictureCount;  //全ての写真の合計枚数
    private int OldPictureCount;  //古い材の写真の合計枚数
    private int NewPictureCount;  //新しい材の写真の合計枚数
    private List<string> AnswerList;  //回答を記録
    private List<string> CorrectAnswerList;  //解答を記録

    private List<string> OXlist;  //OXを記録

    void Start()
    {
        writeTxtList = new List<string>();

        QuestionNumber = QuestionManager.QuestionNumber;
        PictureNumberArray = QuestionManager.PictureNumberArray;
        PictureCount = QuestionManager.PictureCount;
        OldPictureCount = QuestionManager.OldPictureCount;
        NewPictureCount = QuestionManager.NewPictureCount;
        AnswerList = QuestionManager.AnswerList;
        CorrectAnswerList = QuestionManager.CorrectAnswerList;

        OXlist = new List<string>();
    }

    public void ResultExport()
    {
        for (int i = 0; i < CorrectAnswerList.Count; i++)
        {
            if(CorrectAnswerList[i] == AnswerList[i])
            {
                OXlist.Add("〇");
            }
            else
            {
                OXlist.Add("✕");
            }
        }

        for (int i = 0; i < CorrectAnswerList.Count; i++)
        {
            //問題番号,写真番号,正解,回答,正誤
            writeTxtList.Add((i+1).ToString() + "," + PictureNumberArray[i] + "," + CorrectAnswerList[i] + "," + AnswerList[i] + "," + OXlist[i]);
        }

        for (int i = 0; i < CorrectAnswerList.Count; i++)
        {
            writeTxt = writeTxtList[i];

            //結果を書き出し
            Debug.Log(writeTxtList[i]);
            path = Application.dataPath + "/" + fileName;
            //Debug.Log(path);
            //ReadFile();

            WriteFile(writeTxt);
        }
    }

    void WriteFile(string txt)
    {
        FileInfo fi = new FileInfo(path);
        using (StreamWriter sw = fi.AppendText())
        {
            sw.WriteLine(txt);
        }
    }

    /*void ReadFile()
    {
        FileInfo fi = new FileInfo(path);
        try
        {
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                string readTxt = sr.ReadToEnd();
                Debug.Log(readTxt);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }*/

}