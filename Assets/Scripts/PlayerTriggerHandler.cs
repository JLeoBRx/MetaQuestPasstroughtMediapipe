using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    public GameObject[] gasLevels; // 0: cheio, 1: gasto, 2: metade, 3: acabando
    public GameObject player;
    public GameObject playerMorto;
    public GameObject asaEsquerda;
    public GameObject asaDireita;
    public GameObject canvasFase1;
    public GameObject canvasFase2;
   public GameObject galaodegas;

    private int gasIndex = 0;
    private float gasTimer = 0f;
    public float gasInterval = 10f;

    private Vector3 initialPlayerMovePosition;
    private Vector3 initialPlayerMortoPosition;
    private Vector3 initialAsaEsquerdaPosition;
    private Vector3 initialAsaDireitaPosition;

    void Start()
    {
       initialPlayerMovePosition = transform.position;
       initialPlayerMortoPosition = playerMorto.transform.position;
       initialAsaEsquerdaPosition = asaEsquerda.transform.position;
       initialAsaDireitaPosition = asaDireita.transform.position;


        AtivarNivelGas(0);
    }

    void Update()
    {
        gasTimer += Time.deltaTime;

        if (gasTimer >= gasInterval)
        {
            gasTimer = 0f;
            ConsumirGas();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Coletou galão de gás
   //     if (other.CompareTag("Galaodegas"))
     //   {
       //     Debug.Log("Entrou no GalaodeGas");
         //   if (gasIndex > 0)
         //   {
           //     AtivarNivelGas(gasIndex - 1); // Recupera um nível
          //  }

//            galaodegas.SetActive(false); // Desativa galão
  //      }

        // Tocou na ave = morreu
    //    else if (other.CompareTag("Ave"))
      //  {
        //    Morrer();
       // }

        // Chegou ao final da fase
   //     else if (other.CompareTag("Chegada"))
     //   {
       //     canvasFase1.SetActive(false);
         //   canvasFase2.SetActive(true);
        //}
    }

 
    void AtivarNivelGas(int novoIndice)
    {
        gasLevels[gasIndex].SetActive(false);
        gasIndex = Mathf.Clamp(novoIndice, 0, gasLevels.Length - 1);
        gasLevels[gasIndex].SetActive(true);
    }

    void ConsumirGas()
    {
        if (gasIndex < gasLevels.Length - 1)
        {
            AtivarNivelGas(gasIndex + 1);
        }
        else
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        player.SetActive(false);
        playerMorto.SetActive(true);

        // Chama o respawn em 1 segundo
        Invoke(nameof(Respawn), 1f);
    }


    public void PegouGas()
    {
         Debug.Log("Entrou no GalaodeGas");
            if (gasIndex > 0)
            {
                AtivarNivelGas(gasIndex - 1); // Recupera um nível
            }

        //    galaodegas.SetActive(false); // Desativa galão
   
    }




    void Respawn()
{
    // Reativa o player
    player.SetActive(true);
       transform.position = initialPlayerMovePosition;
        playerMorto.transform.position = initialPlayerMortoPosition;
        asaEsquerda.transform.position = initialAsaEsquerdaPosition;
        asaDireita.transform.position = initialAsaDireitaPosition;
        // Desativa o morto
    playerMorto.SetActive(false);
    asaEsquerda.SetActive(false);
    asaDireita.SetActive(false);
    

    // Gás volta para cheio
        AtivarNivelGas(0);

    // Reativa o galão
    galaodegas.SetActive(true);

    // Reseta os canvas
    canvasFase2.SetActive(false);
    canvasFase1.SetActive(true);
}

}
