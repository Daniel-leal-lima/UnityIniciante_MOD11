using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeletorDeEnigma : MonoBehaviour
{
    [SerializeField] ListaDeEnigmas lista;
    [SerializeField] Text perguntaTexto;
    [SerializeField] Text botao1Texto;
    [SerializeField] Text botao2Texto;
    [SerializeField] Text botao3Texto;
    [SerializeField] Text botao4Texto;
    [SerializeField] Text scoreTexto;
    [SerializeField] Text recordText;
    [SerializeField] Text dificuldadeText;
    [SerializeField] Text powerUpText;


    List<string> respostasPossiveis = new List<string>();
    int index;
    int score;
    int record;
    static int usoMaxPowerUp = 3;
    int usosPowerUp = usoMaxPowerUp;
    private void Start()
    {
        scoreTexto.text = "Score: " + score.ToString();
        record = PlayerPrefs.GetInt("record", 0);
        recordText.text = "Record: " + record.ToString();
        powerUpText.text = "Power-Up(" + usosPowerUp + "/" + usoMaxPowerUp + ")";
        index = Random.Range(0, lista.listaDeEnigmas.Count);

        if (lista.listaDeEnigmas.Count > 0)
        {
            dificuldadeText.text = PegaDificuldade(lista.listaDeEnigmas[index].dificuldade);

            respostasPossiveis.Add(lista.listaDeEnigmas[index].respCorreta);
            respostasPossiveis.Add(lista.listaDeEnigmas[index].respErrada1);
            respostasPossiveis.Add(lista.listaDeEnigmas[index].respErrada2);
            respostasPossiveis.Add(lista.listaDeEnigmas[index].respErrada3);

            int indexResposta = Random.Range(0, respostasPossiveis.Count);

            perguntaTexto.text = lista.listaDeEnigmas[index].pergunta;

            botao1Texto.text = respostasPossiveis[indexResposta];
            respostasPossiveis.Remove(respostasPossiveis[indexResposta]);
            indexResposta = Random.Range(0, respostasPossiveis.Count);

            botao2Texto.text = respostasPossiveis[indexResposta];
            respostasPossiveis.Remove(respostasPossiveis[indexResposta]);
            indexResposta = Random.Range(0, respostasPossiveis.Count);

            botao3Texto.text = respostasPossiveis[indexResposta];
            respostasPossiveis.Remove(respostasPossiveis[indexResposta]);
            indexResposta = Random.Range(0, respostasPossiveis.Count);

            botao4Texto.text = respostasPossiveis[indexResposta];
            respostasPossiveis.Remove(respostasPossiveis[indexResposta]);
        }
        else
        {
            Debug.Log("Parabéns! Você acertou todas as perguntas");
        }
    }

    public void OnClick(Text TextoBotao)
    {
        if(TextoBotao.text == lista.listaDeEnigmas[index].respCorreta)
        {
            Debug.Log("RESPOSTA CORRETA!");
            AudioManager.instance.PlayAudio(AudioManager.IDCORRECT);
            lista.listaDeEnigmas.Remove(lista.listaDeEnigmas[index]);
            score += 5;
            scoreTexto.text = "Score: " + score.ToString();

            if (score > record)
            {
                record = score;
                recordText.text = "Record: " + record;
                PlayerPrefs.SetInt("record", record);
            }
            Start();
        }
        else
        {
            Debug.Log("RESPOSTA ERRADA!");
            AudioManager.instance.PlayAudio(AudioManager.IDWRONG);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UsaPowerUp()
    {
        if (usosPowerUp > 0)
        {
            Debug.Log("USANDO POWER UP");
            AudioManager.instance.PlayAudio(AudioManager.IDPOWERUP);
            usosPowerUp--;
            powerUpText.text = "Pula Questão(" + usosPowerUp + "/" + usoMaxPowerUp + ")";
            lista.listaDeEnigmas.Remove(lista.listaDeEnigmas[index]);
            Start();

        }
        if (usosPowerUp.Equals(0))
        {
            powerUpText.GetComponentInParent<Button>().interactable = false;
        }
    }

    string PegaDificuldade(int i)
    {
        string res;
        switch (i)
        {
            case 0: res = "Fácil"; break;
            case 1: res = "Médio"; break;
            case 2: res = "Difícil"; break;
            default: res = "Não definida"; break;
        }
        return res;

    }
}
