using UnityEngine;
using System.Collections;

public class MoveFundo : MonoBehaviour { 

	private float larguraTela;

	void Start () { 
		GetComponent<Rigidbody2D>().velocity = new Vector2(-3,0);//ele vai para a esquerda
		SpriteRenderer grafico = GetComponent<SpriteRenderer>();//para ser dinamico
        //e para a imagem chegar no lado esquerda (fim da tela) ele vai para o outro lado(loop)
		float larguraImagem = grafico.sprite.bounds.size.x;//tamanho
		float alturaImagem = grafico.sprite.bounds.size.y;//tamanho

		//		print(larguraImagem);
		//		print(alturaImagem);

		float alturaTela = Camera.main.orthographicSize*2f; // metade do tamanho da camera/tamanho da tela

		//		Camera's half-size when in orthographic mode.
		//		This is half of the vertical size of the viewing volume. 
		//		Horizontal viewing size varies depending on viewport's aspect ratio.

		larguraTela = alturaTela/Screen.height*Screen.width;  //altura/largura*
		//		The current height of the screen window in pixels

		Vector2 novaEscala = transform.localScale;  //criou
		novaEscala.x = larguraTela / larguraImagem + 0.25f; 
		novaEscala.y = alturaTela / alturaImagem;
		transform.localScale = novaEscala; //redimenciona

		if (this.name == "ImagemFundoA2"){ // pergunta o nome
			transform.position = new Vector2(larguraTela,0f); // reposiciona
		}
	}  

	void Update () { 
		if(transform.position.x <= -larguraTela){ // para fazer o loop
			transform.position = new Vector2(larguraTela,0f); //reposiçao da tela
		}
	} 
}