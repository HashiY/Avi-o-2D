using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour {

	private bool comecouJogo;
	private bool acabouJogo; 
	private int pontuacao;
	public Text textoScore;
	GameObject objetoGameEngine;

	Vector2 forcaImpulso = new Vector2(0,500);

	public GameObject objetoParticulasPenas;
 
	void Start () {
		objetoGameEngine = GameObject.FindGameObjectWithTag("MainCamera");

		float larguraTela = (Camera.main.orthographicSize*2f)/Screen.height*Screen.width;   
		transform.position = new Vector2(-larguraTela/4,0f); 
 
		textoScore.transform.position = new Vector2(Screen.width/2,Screen.height - 250);  //onde colocar
		textoScore.text = "Toque para iniciar!";
		textoScore.fontSize = 35; 
	} 

	void Update () { 
		if(!acabouJogo){
			if(Input.GetButtonDown("Fire1")){  //mause
				if(!comecouJogo){ // se nao começou
					comecouJogo=true;
					GetComponent<Rigidbody2D>().isKinematic = false; // desliga

                    textoScore.transform.position = new Vector2(Screen.width / 3, Screen.height - 150);
                    textoScore.text = pontuacao.ToString(); // transformando para string
					textoScore.fontSize = 50;
					textoScore.color = new Color(0.95f,1.0f,0.35f);
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
		pontuacao++;
		textoScore.text = pontuacao.ToString(); // atualizar a pontuaçao
		print(pontuacao);
	}

	void RecarregaCena(){
		Application.LoadLevel("aviao");
	}
}
