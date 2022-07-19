using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {

	private string sceneName;
	private bool comecouJogo;
	private bool acabouJogo; 
	private int score, highscore;
	public Text scoreText;
	public Text startText;
	public Text entryNameText;
	public Text entryScoreText;
	public GameObject objetoCanvas;
	public GameObject objetoCanvasHighscore;
	GameObject objetoGameEngine;
	RectTransform rectTransform;

	Vector2 forcaImpulso = new Vector2(0,300); //

	public GameObject objetoParticulasPenas;
 
	void Start () {
		objetoGameEngine = GameObject.FindGameObjectWithTag("MainCamera");

		float larguraTela = (Camera.main.orthographicSize*2f)/Screen.height*Screen.width;   
		transform.position = new Vector2(-larguraTela/4,0f);

		startText.transform.position = new Vector2(Screen.width / 2, Screen.height - 100);
		startText.text = "Toque para iniciar!";
		startText.fontSize = 35;
		startText.color = new Color(0.95f, 1.0f, 0.35f);

		objetoCanvas.transform.position = new Vector2(Screen.width/6 - 50, Screen.height/6);
		scoreText.text = "0";
		scoreText.fontSize = 40;
		scoreText.color = new Color(0.95f, 1.0f, 0.35f);
		rectTransform = scoreText.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(400, 200);

		objetoCanvasHighscore.transform.position = new Vector2(Screen.width/6 - 50, Screen.height/6 - 50);
		entryNameText.color = new Color(0.95f,1.0f,0.35f);
		entryScoreText.color = new Color(0.95f,1.0f,0.35f);
		entryScoreText.fontSize = 40;

		//Colocando o highscore para ser salvo
		sceneName = SceneManager.GetActiveScene().name;
		if(PlayerPrefs.HasKey(sceneName + "score"))
        {
			highscore = PlayerPrefs.GetInt(sceneName + "score");
			entryScoreText.text = highscore.ToString();
		}
	} 

	void Update () { 
		if(!acabouJogo){
			if (Input.GetButtonDown("Fire1")){  //mause
				if(!comecouJogo){ // se nao começou
					comecouJogo=true;
					GetComponent<Rigidbody2D>().isKinematic = false; // desliga

					startText.text = "";
					scoreText.text = score.ToString(); // transformando para string
                    objetoGameEngine.SendMessage("Comeca");
                }
				GameObject particula = Instantiate(objetoParticulasPenas); // aparece as penas da prefab
				particula.transform.position = this.transform.position; 
				GetComponent<Rigidbody2D>().velocity = Vector2.zero; // zera a velocidade
				GetComponent<Rigidbody2D>().AddForce(forcaImpulso);  //clica sobe
			}

			transform.rotation = Quaternion.Euler(0,0,GetComponent<Rigidbody2D>().velocity.y * 3f);//para impinar

			float posicaoFelpudoEmPixels = Camera.main.WorldToScreenPoint(transform.position).y;//conveetendo a unidade da unity para pixel

			if(posicaoFelpudoEmPixels > Screen.height || posicaoFelpudoEmPixels <0){ //maior ou menor da tela morre
				if(!acabouJogo){
					GetComponent<SpriteRenderer>().color = new Color(1f,0.75f,0.75f,1.0f);
					acabouJogo = true;
					FimDejogo();
				}
//				Destroy(this.gameObject);
			}
		} 
	} 
	void OnCollisionEnter2D()
	{ 
		if(!acabouJogo){
			GetComponent<Collider2D>().enabled = false; // desliga para nao ser enpurrado
			GetComponent<Rigidbody2D>().velocity = Vector2.zero; // zera velocidade
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-400,0)); // pula a esquerda
			GetComponent<Rigidbody2D>().AddTorque(300f); // para girar
			GetComponent<SpriteRenderer>().color = new Color(1f,0.75f,0.75f,1.0f); // muda cor
			acabouJogo = true; // acaba
			Invoke("FimDejogo", 1);
		}
	} 
	void FimDejogo()
	{
		objetoGameEngine.SendMessage("FinalizarJogo"); // carrega a funçao
		Invoke("RecarregaCena", 2); // depois de 2s
	} 

	void Pontua(){
		score++;
		scoreText.text = score.ToString(); // atualizar a pontuaçao

		if (score > highscore)
		{
			print(score);
			highscore = score;
			entryScoreText.text = highscore.ToString();
			PlayerPrefs.SetInt(sceneName + "score", highscore);
		}
	}

	void RecarregaCena(){
		Application.LoadLevel("aviao");
	}
}
