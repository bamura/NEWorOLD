using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public static int QuestionNumber;  //設問番号、何問目かを示す通し番号
    private int PictureNumber;  //QuestionPictureArrayの中の何番目の写真なのかを示す
    public static int[] PictureNumberArray;      //ランダムな数字(PictureNumber)が被りなく順に並んでいる配列、indexはQuestionNumber-1を示す
    [SerializeField] private GameObject camera;
    [SerializeField] private Button NewButton;
    [SerializeField] private Button OldButton;
    public static List<string> AnswerList;  //回答を記録
    public static List<string> CorrectAnswerList;  //解答を記録
    [SerializeField] private Button EndButton;
    [SerializeField] private GameObject FinishScreen;
    [SerializeField] private GameObject ResultExporterObj;
    [SerializeField] private float cameraZpos;
    [SerializeField] private List<Sprite> OldMaterialsList;
    [SerializeField] private List<Sprite> NewMaterialsList;
    private List<Sprite> AllMaterialsList;  //前半は古い材、後半は新しい材、で構成されたすべての画像のリスト
    public static int PictureCount;  //全ての写真の合計枚数
    public static int OldPictureCount;  //古い材の写真の合計枚数
    public static int NewPictureCount;  //新しい材の写真の合計枚数
    [SerializeField] private GameObject[] QuestionPictureArray;  //AllMaterialsListが順にインスタンス化されて順に配列に入れられている。

    // Start is called before the first frame update
    void Start()
    {
        QuestionNumber = 1;
        AllMaterialsList = new List<Sprite>();

        var SW = Screen.width;
        var SH = Screen.height;
        cameraZpos = 50f;
        camera.transform.position = new Vector3(SW / 2, SH / 2, -cameraZpos);

        //AllMaterialsListの前半は古い材、後半は新しい材、の順で入れておく
        for (int n = 0; n < OldMaterialsList.Count; n++)
        {
            AllMaterialsList.Add(OldMaterialsList[n]);
        }
        for (int n = 0; n < NewMaterialsList.Count; n++)
        {
            AllMaterialsList.Add(NewMaterialsList[n]);
        }

        PictureCount = AllMaterialsList.Count;
        OldPictureCount = OldMaterialsList.Count;
        NewPictureCount = NewMaterialsList.Count;

        QuestionPictureArray = new GameObject[PictureCount];
        for (int n = 0; n < PictureCount ; n++)
        {
            //Material0,1,...として画像を作り、QuestionPictureArrayに順に挿入
            Sprite QuestionPictureSprite = AllMaterialsList[n];
            GameObject QuestionPicture = new GameObject("Material" + n.ToString());
            QuestionPicture.AddComponent<SpriteRenderer>().sprite = QuestionPictureSprite;
            QuestionPicture.transform.position = new Vector3(SW/2 , SH/2 , 0f);
            QuestionPictureArray[n] = QuestionPicture;
            QuestionPicture.SetActive(false);
        }

        PictureNumberArray = new int[PictureCount];

        //PictureNumberArrayに被りなくすべての写真番号をランダムに入れていく
        List<int> numbers = new List<int>();  //0,1,2,3,...と順に入ってるだけ
        for (int i = 0; i < PictureCount; i++)
        {
            numbers.Add(i);
        }
        int counter = 0;
        while (numbers.Count > 0)
        {
            int index = Random.Range(0, numbers.Count);  //Maxは含まない
            PictureNumberArray[counter] = numbers[index];
            counter++;
            numbers.RemoveAt(index);
        }
        PictureNumber = PictureNumberArray[QuestionNumber - 1];  //QuestionNumberは１から始まる
        QuestionPictureArray[PictureNumber].SetActive(true);
        FinishScreen.SetActive(false);

        AnswerList = new List<string>();
        CorrectAnswerList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OldButtonClick()
    {
        if (0 <= PictureNumber && PictureNumber < OldPictureCount)
        {
            CorrectAnswerList.Add("Old");
        }
        if (OldPictureCount <= PictureNumber && PictureNumber < PictureCount)
        {
            CorrectAnswerList.Add("New");
        }
        QuestionPictureArray[PictureNumber].SetActive(false);
        QuestionNumber++;
        if (QuestionNumber == PictureCount + 1)
        {
            AnswerList.Add("Old");
            NewButton.gameObject.SetActive(false);
            OldButton.gameObject.SetActive(false);
            EndButton.gameObject.SetActive(false);
            FinishScreen.SetActive(true);
            //ResultExporter内の関数を起動
            ResultExporter resultExporter = ResultExporterObj.GetComponent<ResultExporter>();
            resultExporter.ResultExport();
        }
        else
        {
            PictureNumber = PictureNumberArray[QuestionNumber - 1];
            QuestionPictureArray[PictureNumber].SetActive(true);
            AnswerList.Add("Old");
        }
    }

    public void NewButtonClick()
    {
        if (0 <= PictureNumber && PictureNumber < OldPictureCount)
        {
            CorrectAnswerList.Add("Old");
        }
        if (OldPictureCount <= PictureNumber && PictureNumber < PictureCount)
        {
            CorrectAnswerList.Add("New");
        }
        QuestionPictureArray[PictureNumber].SetActive(false);
        QuestionNumber++;
        if (QuestionNumber == PictureCount + 1)
        {
            AnswerList.Add("New");
            NewButton.gameObject.SetActive(false);
            OldButton.gameObject.SetActive(false);
            EndButton.gameObject.SetActive(false);
            FinishScreen.SetActive(true);
            //ResultExporter内の関数を起動
            ResultExporter resultExporter = ResultExporterObj.GetComponent<ResultExporter>();
            resultExporter.ResultExport();
        }
        else
        {
            PictureNumber = PictureNumberArray[QuestionNumber - 1];
            QuestionPictureArray[PictureNumber].SetActive(true);
            AnswerList.Add("New");
        }
    }

    public void EndButtonClick()
    {
        QuestionPictureArray[PictureNumber].SetActive(false);
        NewButton.gameObject.SetActive(false);
        OldButton.gameObject.SetActive(false);
        EndButton.gameObject.SetActive(false);
        FinishScreen.SetActive(true);
        //ResultExporter内の関数を起動
        ResultExporter resultExporter = ResultExporterObj.GetComponent<ResultExporter>();
        resultExporter.ResultExport();
    }
}
