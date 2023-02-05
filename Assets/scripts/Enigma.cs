using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enigma
{
    [SerializeField] public string pergunta;
    [SerializeField] public string respCorreta;
    [SerializeField] public string respErrada1;
    [SerializeField] public string respErrada2;
    [SerializeField] public string respErrada3;
    // dificuldade : 0 - fácill | 1- normal | 2 - dificil
    [SerializeField] public int dificuldade;
    
    

}
