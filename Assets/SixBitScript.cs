using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using static SixBitExtensionsNamespace.SixBitExtensions;

public class SixBitScript : MonoBehaviour
{

    public TextMesh symbol;
    public KMSelectable hover;
    public KMSelectable button0, button1, commandScreen, binaryScreen;
    public TextMesh commandText, binaryText;
    public GameObject cursorRenderer;
    
    private List<TextMesh> symbols = new List<TextMesh>();
    private List<KMSelectable> hovers = new List<KMSelectable>();
    
    private List<int> data = new List<int>();
    private List<int> colortable = new List<int>();
    private List<Color> colors = new List<Color>();
    private bool ModuleSolved, highlighted;

    private string currentInputtedBinary = "";
    private int cursor;

    private Coroutine currentCorout;

    private string currentMode = "DATA";
    private string insertionMode = "REPLACE";

    private AsmHandler asmHandler = new AsmHandler();
    private TaskChecker taskChecker;
    private bool noteMode;

    private static int ModuleIdCounter;
    private int ModuleId;

    void Awake() { ModuleId = ++ModuleIdCounter;}
    void log(string msg) { Debug.Log($"[6-Bit Assembly #{ModuleId}] {msg}");}

    
    List<int> getParameters(List<int> colorValues){
        int index = 6;
        if(GetComponent<KMBombInfo>().GetSerialNumberLetters().Count(x=>"AEIOU".Contains(x))>0)
            index = GetComponent<KMBombInfo>().GetSerialNumber().IndexOf(GetComponent<KMBombInfo>().GetSerialNumber().ToCharArray().First(x=>"AEIOU".Contains(x)));
        else if (GetComponent<KMBombInfo>().GetSerialNumberNumbers().Count(x=>x%2==0)>0)
            index = GetComponent<KMBombInfo>().GetSerialNumber().IndexOf(GetComponent<KMBombInfo>().GetSerialNumber().ToCharArray().First(x=>"02468".Contains(x)));
        List<int> intermediateValues = colorValues.Where((_,i)=>i%8==index).ToList();
        for (int i=1; i<8; i++){
            while (intermediateValues.Take(i).Select(x=>x%8).ToList().Contains(intermediateValues[i]%8)) intermediateValues[i]++;
        }
        return intermediateValues.Select(x=>x%64).Select((x,i)=>x==0 && i>0?8:x).ToList();
    }

    void setCursor(int index){
        if (index == -1)
        {
            cursorRenderer.SetActive(false);
            currentMode = "DATA";
        }
        else{
            cursorRenderer.SetActive(true);
            cursorRenderer.transform.position = symbols[index%64].transform.position;
            cursor = index;
            currentMode = index>63?"PROG1":"PROG0";
        }

    }
    void pressSymbol(int index)
    {
        if (ModuleSolved && !noteMode) return;
        if (currentCorout != null) return;
        if (currentMode != "DATA") setCursor(index + (currentMode == "PROG1"?64:0));
    }
    void hoverSymbol(int index){
        if (ModuleSolved) return;
        if (currentCorout != null) return;
        switch (currentMode){
            case "DATA": 
                commandText.text = $"e{decToOct(data[index],2)}{decToOct(colortable[index],2)}";
                break;
            case "PROG0":
                commandText.text = asmHandler.commandToString(asmHandler.program[index]);
                break;
            case "PROG1":
                commandText.text = asmHandler.commandToString(asmHandler.program[index+64]);
                break;
            default: return;
        }
    }

    void hoverSymbolEnded()
    {
        if (currentCorout != null) return;
        if (currentMode == "DATA")
        {
            commandText.text = "";
            return;
        }
        if (currentInputtedBinary.Length == 12) commandText.text = asmHandler.commandToString(currentInputtedBinary);
        else
        {
            commandText.text = asmHandler.commandToString(asmHandler.program[cursor]);
        }
    }

    void prepareNoteMode(){
        noteMode = true;
        currentMode="PROG0";
        render();
    }

    void pressCommand(){
        //if seeing data - run code; if seeing program and input is "" - change insertion mode; if seeing program and input is not "" - clear input
        //if run() - stop
        if (ModuleSolved) prepareNoteMode();
        if (currentCorout != null)
        {
            killCorout();
            binaryText.text = "";
            return;
        }
        if (currentMode == "DATA"){
            if (!noteMode) currentCorout = StartCoroutine(run());
        }
        else if (currentInputtedBinary != ""){
            currentInputtedBinary = "";
            binaryText.text = "";
        }
        else{
            insertionMode = insertionMode == "REPLACE"?"INSERT":"REPLACE";
            commandText.text = insertionMode;
        }
    }
    void pressBinary()
    {
        if (ModuleSolved && !noteMode) return;
        if (currentCorout != null) return;
        //if seeing data - go to prog0, if seeing prog and input is "" - go to data, if input is "0" - delete @ cursor, input "1" - change page, else nothing
        if (currentMode == "DATA"){
            currentMode = "PROG0";
        }
        else if (currentInputtedBinary == "")
        {
            if (!noteMode) currentMode = "DATA";
        }
        else if (currentInputtedBinary == "0") deleteAtCursor();
        else if (currentInputtedBinary == "1") setCursor((cursor + 64) % 128);
        render();
    }
    void deleteAtCursor(){
        asmHandler.program.RemoveAt(cursor);
        if (asmHandler.program.Count < 128) asmHandler.program.Add("000000000000");
    }
    void render()
    {
        print($"current mode: {currentMode}");
        setCursor(currentMode == "DATA"?-1:cursor);
        List<int> n = currentMode == "DATA"?asmHandler.asm6.data:
            currentMode=="PROG0"?asmHandler.program.Take(64).Select(s => fromBinary(s.Substring(0,6))).ToList():
            asmHandler.program.Take(128).TakeLast(64).Select(s => fromBinary(s.Substring(0,6))).ToList();
        List<int> c = currentMode == "DATA"?colortable:
            currentMode=="PROG0"?asmHandler.program.Take(64).Select(s => fromBinary(s.Substring(6,6))).ToList():
            asmHandler.program.Take(128).TakeLast(64).Select(s => fromBinary(s.Substring(6,6))).ToList();
        for (int i=0; i<64; i++){
            symbols[i].text = getChar(n[i]).ToString();
            symbols[i].color = colors[c[i]];
            hovers[i].GetComponent<MeshRenderer>().material.color = c[i]==0?Color.gray : Color.clear;
        }

        hoverSymbolEnded();
    }

    IEnumerator run(int iteration = 0){
        if(iteration==0)
        {
            
        List<int> initialRegisters = Enumerable.Range(0,8).Select(_ => UnityEngine.Random.Range(0,64)).ToList();
        asmHandler.init(data, initialRegisters);
        taskChecker.loadData(initialRegisters, data);
        yield return null;
        }
        int nonPointerCounter = 0, pointerCounter = 0, allCounter = 0;
        log($"Starting iteration {iteration}.");
        while (true){
            List<int> dataRun = asmHandler.asm6.data.ToList();
            List<int> registersRun = asmHandler.asm6.registers.ToList();
            int pointerRun = asmHandler.asm6.counter;
            setCursor(pointerRun);
            if (iteration == 0 || allCounter%256==0){
                commandText.text = asmHandler.commandToString(asmHandler.program[pointerRun]);
                binaryText.text = asmHandler.registersToString().Replace("\t", "    ");
                render();
            }
            if (iteration == 0) log($"PC: {pointerRun}, current command: {asmHandler.commandToString(asmHandler.program[pointerRun])}, registers/flags: {asmHandler.registersToString().Replace("\t","_")}.");
            bool runBool = asmHandler.run();
            if (!runBool){
                GetComponent<KMBombModule>().HandleStrike();
                currentCorout = null;
                yield break;
            }
            if (
                asmHandler.asm6.data.SequenceEqual(dataRun) &&
                asmHandler.asm6.registers.SequenceEqual(registersRun)
            ){
                if (asmHandler.asm6.counter == pointerRun) pointerCounter++;
                else pointerCounter = 0;
                nonPointerCounter++;
            }
            else{
                nonPointerCounter = 0;
                pointerCounter = 0;
            }

            allCounter++;
            if (allCounter >= 65536) {currentCorout = null;
                yield break;}
            if(pointerCounter >= 8 || nonPointerCounter >= 128)
            {
                string errorCode = taskChecker.checkSolution(registersRun, dataRun);
                if (errorCode == ""){
                    if (iteration == 5){
                        log("Solving the module.");
                        StartCoroutine(solve());
                    }
                    else{
                        List<int> initialRegisters1 = Enumerable.Range(0,8).Select(_ => UnityEngine.Random.Range(0,64)).ToList();
                        List<int> data1 = Enumerable.Range(0,64).Select(_ => UnityEngine.Random.Range(1,64)).ToList();
                        asmHandler.init(data1, initialRegisters1);
                        taskChecker.loadData(initialRegisters1, data1);
                        yield return run(iteration+1);
                    }
                }
                else
                {
                    log(errorCode);
                }
                currentCorout = null;
                yield break;
            }

            if (iteration == 0) yield return new WaitForSeconds(.2f);
            else if (allCounter%256==0) yield return null;
        }
    }

    void killCorout(){
        if (currentCorout != null) StopCoroutine(currentCorout);
        currentCorout = null;
    }

    IEnumerator solve(){
        killCorout();
        string solveMessage = "c  bb  c c    c  ca!!ac cMODULEccSOLVEDc ca!!ac  c    c c  bb  c", 
               colorMessage = "a  II  c b    b  c[44[a b))))))ba))))))c b[44[b  c    a b  II  b".Replace(' ','_');
        int row = 0;
        ModuleSolved = true;
        GetComponent<KMBombModule>().HandlePass();
        while (!noteMode){
            for (int j=0; j<8; j++) {
                for (int i=0; i<7; i++){
                symbols[i*8+j].text = symbols[i*8+j+8].text;
                symbols[i*8+j].color = symbols[i*8+j+8].color;
                }
                symbols[56+j].text = solveMessage[row*8+j].ToString();
                symbols[56+j].color = colors[getInt(colorMessage[row*8+j])];
            }
            row = (row+1)%8;
            yield return new WaitForSeconds(.5f);
        }
    }

    void initBoard(){
        colors = Enumerable.Range(0,64).Select(i => new Color(i / 16 / 3f, ((i / 4) % 4) / 3f, (i % 4) / 3f)).ToList();
        //colors[0] = new Color(1, 1, 1) / 5f;
        for (int i=0; i<64; i++)
        {
            int n = UnityEngine.Random.Range(1, 64);
            data.Add(n);
            int c = UnityEngine.Random.Range(0, 64);
            colortable.Add(c);
            int i1=i;
            hovers[i1].OnInteract += delegate{pressSymbol(i1); return false;};
            hovers[i1].OnHighlight += delegate{hoverSymbol(i1);};
            hovers[i1].OnHighlightEnded += hoverSymbolEnded;
        }
        log($"Colors: {colortable.Select(c => getChar(c).ToString()).Aggregate((a,b)=>a+b)}");
    }

    void pressNumber(int num)
    {
        if (ModuleSolved && !noteMode) return;
        if (currentCorout != null) return;
        if (currentMode == "DATA") return;
        if (currentInputtedBinary.Length < 12)
        {
            currentInputtedBinary += num.ToString();
            binaryText.text = currentInputtedBinary.ToCharArray().Select((x,i)=>x.ToString() + (i%3==2?" ":"")).Aggregate((a, b) => a + b);
            if (currentInputtedBinary.Length == 12) commandText.text = asmHandler.commandToString(currentInputtedBinary);
        }
        else
        {
            if (num == 1)
            {
                if (insertionMode == "INSERT")
                    asmHandler.program.Insert(cursor, currentInputtedBinary);
                else asmHandler.program[cursor] = currentInputtedBinary;
                if (cursor%64 != 63) setCursor(cursor+1);
            }
            commandText.text = "";
            binaryText.text = "";
            currentInputtedBinary = "";
            render();
        }
    }

    void Start () {
        symbols.Add(symbol);
        hovers.Add(hover);
        for (int i = 1; i < 64; i++)
        {
            TextMesh symbolCopy = Instantiate(symbol, transform);
            symbolCopy.transform.localPosition += new Vector3(i % 8 * .017f,0, i / 8 *-.017f);
            symbols.Add(symbolCopy);
            //KMSelectable hoverCopy = Instantiate(hover, symbolCopy.transform);
            hovers.Add(symbolCopy.GetComponentInChildren<KMSelectable>());
            //hoverCopy.transform.localPosition += new Vector3(i % 8 * .017f, 0, i / 8 *-.017f);
        }
        GetComponent<KMSelectable>().Children = Enumerable.Range(-4, 68).Select(i =>
        {
            switch (i)
            {
                case -4: return button0;
                case -3: return button1;
                case -2: return commandScreen;
                case -1: return binaryScreen;
                default: return hovers[i];
            }
        }).ToArray();
        GetComponent<KMSelectable>().UpdateChildrenProperly();
        //float offset = 0.017f;
        
        GetComponent<KMSelectable>().OnFocus+=delegate{highlighted = true;};
        GetComponent<KMSelectable>().OnDefocus+=delegate{highlighted = false;};
        commandScreen.OnInteract += delegate{pressCommand(); return false;};
        binaryScreen.OnInteract += delegate{pressBinary(); return false;};
        button0.OnInteract += delegate { pressNumber(0); return false;};
        button1.OnInteract += delegate { pressNumber(1); return false;};
        initBoard();
        asmHandler.asm6.data = data.ToList();
        asmHandler.program = Enumerable.Repeat("000000000000",128).ToList();
        render();
        commandText.text = "";
        binaryText.text = "";
        taskChecker = new TaskChecker(getParameters(colortable));
	}
	
	
	void Update () {
		if ((ModuleSolved && !noteMode) || !highlighted) return;
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) button0.OnInteract();
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) button1.OnInteract();}

    private string TwitchHelpMessage = "Use !{0} 0/1/C/B to press 0, 1, Command and Binary. Presses may be chained.";

    IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;
        command = command.ToUpperInvariant().Replace(" ", "");
        if (!Regex.IsMatch(command,"^[01BC]+$"))
        {
            yield return "sendtochaterror Invalid command.";
            yield break;
        }

        yield return "solve";
        yield return "strike";
        foreach (char c in command)
        {
            switch (c)
            {
                case '0': button0.OnInteract(); break;
                case '1': button1.OnInteract(); break;
                case 'B': binaryScreen.OnInteract(); break;
                case 'C': commandScreen.OnInteract(); break;
                default: yield return "sendtochaterror Invalid command."; break;
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        StartCoroutine(solve());
    }
}
