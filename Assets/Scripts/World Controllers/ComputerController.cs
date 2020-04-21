using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ComputerController : Singleton<ComputerController>
{
    public GameObject Computer;
    public GameObject Screen;

    private float index;
    public GameObject indexF;
    private Text indexT;

    private float M0;
    public GameObject M0F;
    private Text M0T;
    
    private float MB;
    public GameObject MBF;
    private Text MBT;
    
    private float M1;
    public GameObject M1F;
    private Text M1T;
    
    private float M2;
    public GameObject M2F;
    private Text M2T;
    
    private float M3;
    public GameObject M3F;
    private Text M3T;
    
    private float MZM;
    public GameObject MZMF;
    private Text MZMT;

    private float unemployment;
    public GameObject unemploymentF;
    private Text unemploymentT;

    private float liquditiy;
    public GameObject liquidityF;
    private Text liquidityT;

    private long population;
    public GameObject populationF;
    private Text populationT;

    private float paranoia;
    public GameObject paranoiaF;
    private Text paranoiaT;

    private List<float> industryValues;
    private List<string> industries;
    public List<GameObject> industriesF;
    public List<Text> industriesT;

    private List<string> currencies = new List<string> {"$","£","¥","€"};
    private List<float> currencyValue = new List<float> {1f,1.25f,0.0093f,1.09f};
    private int currentCurrency = 0;
    private List<string> oom = new List<string> {"","K","M","B","T","QD","QNT","SXT","SPT"};

    private List<string> randomNews = new List<string>
    {
        "Capitalism Still Alive.",
        "Avocado on Pizza Has the Internet in a Civil War.",
        "The Invisible Hand Receives the Nobel Peace Prize.",
        "The King in Yellow Missing for More Than a Year.",
        "Cable TV More Alive Than Ever.",
        "Military Community Still Refuses to Acknowledge UFO Despite First Extraterrestrial Athlete Winning the Olympics.",
        "Your Favourite Sitcom Gets Another Season!",
        "Everyone Besides You Is Dead."
    };

    private List<string> randomBlurb = new List<string>
    {
        "Millennials in Shock!",
        "Top 8 Things You Didn't Want to Know. Top 5 Is:",
        "What Your Hedge Fund Manager Doesn't Want You to Know:",
        "According to the Girl on Your Corner Selling Lemonade:",
        "Stay Ahead of the Pack!",
        "Throwback Thursday:",
        "Intelligence Community Whistle Blower Says:",
        "News You Shouldn't Worry About:",
        "The Revolution Is Now!",
        "One More Thing to Worry About:",
        "Too Good to Be True!",
        "Breaking News!"
    };

    private List<string> randomOpinion = new List<string>
    {
        "Experts Worried.",
        "What Does This Mean for the Chancellor's Chances for Reelection?",
        "Markets in Shock.",
        "First Signs of a Bear Market?",
        "First Signs of a Zebra Market?",
        "First Signs of a Bull Market?",
        "Superpowers Tighten Controls.",
        "Nobody Is Surprised.",
        "How Long Will It Last?",
        "This News Again in 407 Years When the Stars Align Once More."
    };
    private Queue<string> newsqueue = new Queue<string>();
    private char[] currentNews;
    private char[] tickerText = new char[87];
    private int CurrentChar;
    public GameObject tickerF;
    private Text tickerT;

    void Awake()
    {
        tickerT = tickerF.GetComponent<Text>();
        for (int i = 0; i < tickerText.Length; i++) tickerText[i] = ' ';
        
        //Initialize Values
        indexT = indexF.GetComponent<Text>();
        M0T = M0F.GetComponent<Text>();
        MBT = MBF.GetComponent<Text>();
        M1T = M1F.GetComponent<Text>();
        M2T = M2F.GetComponent<Text>();
        M3T = M3F.GetComponent<Text>();
        MZMT = MZMF.GetComponent<Text>();
        unemploymentT = unemploymentF.GetComponent<Text>();
        liquidityT = liquidityF.GetComponent<Text>();
        populationT = populationF.GetComponent<Text>();
        paranoiaT = paranoiaF.GetComponent<Text>();
        
        foreach (GameObject industryF in industriesF) industriesT.Add(industryF.GetComponent<Text>());

        index = 4434f;
        M0 = 40750390000000f;
        MB = 44738640000000f;
        M1 = 34566000000000f;
        M2 = 134552000000000f;
        M3 = 154387000000000f;
        MZM = 188777000000000f;
        unemployment = 0.051383f;
        liquditiy = 0.11312f;
        population = 8000000000000;
        paranoia = 0.079f;
        
        //CreateIndustries();

        TimeUnitChange.timeChangeEvent += Passing;
        PlayNews();
    }
    
    public void doRendering(bool newBool)
    {
        gameObject.SetActive(newBool);
        Renderer[] renderers = Computer.GetComponentsInChildren<Renderer>();
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = newBool;
        }
    }

    public void Passing(int currentMoment)
    { 
        //GenerateEvents();
        //UpdateIndustries();
        UpdateParametres();
        //PrintIndustries();
        //PrintParametres();
        
        UpdateTicker();
        
        if (currentMoment % 150 == 0)
        {
            PlayNews();
        }
    }

    private void UpdateParametres()
    {
        indexT.text = MakeNumberString(index,false);
        M0T.text = MakeNumberString(M0,true);
        MBT.text = MakeNumberString(MB,true);
        M1T.text = MakeNumberString(M1,true);
        M2T.text = MakeNumberString(M2,true);
        M3T.text = MakeNumberString(M3,true);
        MZMT.text = MakeNumberString(MZM,true);
        unemploymentT.text = MakeNumberString(unemployment,false);
        liquidityT.text = MakeNumberString(liquditiy,false);
        populationT.text = MakeNumberString(population,false);
        paranoiaT.text = MakeNumberString(paranoia,false);
    }

    private string MakeNumberString(float num, bool isCurrency)
    {
        string numberString;
        int reductionCount = 0;
        while (num > 1000000)
        {
            num /= 1000;
            reductionCount++;
        }

        numberString = num.ToString("N");
        if (reductionCount > 0) numberString += " " + oom[reductionCount];
        if (isCurrency) numberString += " " + currencies[currentCurrency];

        return numberString;
    }
    
    private string MakeNumberString(long num, bool isCurrency)
    {
        string numberString;
        int reductionCount = 0;
        while (num > 1000000000)
        {
            num /= 1000;
            reductionCount++;
        }

        numberString = num.ToString("N");
        if (reductionCount > 0) numberString += " " + oom[reductionCount];
        if (isCurrency) numberString += " " + currencies[currentCurrency];

        return numberString;
    }

    private void PlayNews()
    {
        if (currentNews != null) return;
        if (newsqueue.Count == 0) GenerateNews();
        currentNews = newsqueue.Dequeue().ToCharArray();
        CurrentChar = 0;
    }

    private void GenerateNews()
    {
        int b = Random.Range(0,randomBlurb.Count);
        int n = Random.Range(0,randomNews.Count);
        int o = Random.Range(0,randomOpinion.Count);
        string news = "    >>" + randomBlurb[b] + "  " + randomNews[n] + "  " + randomOpinion[o] + "<<    ";
        newsqueue.Enqueue(news);
    }

    private void UpdateTicker()
    {
        char nextChar = ' ';
        if (currentNews != null)
        {
            if (CurrentChar == currentNews.Length)
            {
                currentNews = null;
            }
            else
            {
                nextChar = currentNews[CurrentChar];
                CurrentChar++;
            }
        }

        for (int i = 0; i < tickerText.Length - 1; i++)
        {
            tickerText[i] = tickerText[i + 1];
        }

        tickerText[tickerText.Length - 1] = nextChar;

        tickerT.text = new string(tickerText);
    }
}
