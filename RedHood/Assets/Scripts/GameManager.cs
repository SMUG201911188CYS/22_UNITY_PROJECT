using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Image GunImage;
    public Image BulletImg;
    public Text BulletCount;
    public Image Item1Img;
    public Image Item2Img;
    public Image Item3Img;
    public Image Item4Img;
    public Image Item5Img;
    public Image Gun0Img;
    public Image Gun1Img;
    public Image Gun2Img;
    public Image Gun3Img;
    public Image Gun4Img;
    public Text Gun3Text;
    public Image Paper1Img;
    public Image Paper2Img;
    public Image Paper3Img;
    public Image PaperAllImg;
    public Text ItemText;


    
    

    void LateUpdate()
    {
        Item1Img.color = new Color(1, 1, 1, player.hasItems[0] ? 1 : 0);
        Item2Img.color = new Color(1, 1, 1, player.hasItems[1] ? 1 : 0);
        Item3Img.color = new Color(1, 1, 1, player.hasItems[2] ? 1 : 0);
        Item4Img.color = new Color(1, 1, 1, player.hasItems[3] ? 1 : 0);
        Item5Img.color = new Color(1, 1, 1, player.hasItems[4] ? 1 : 0);
        
        GunImage.color = new Color(1, (player.equipItem == player.musket) ? 0 : 1, (player.equipItem == player.musket) ? 0 : 1, player.hasMusket ? 0.95f : 0);
        BulletImg.color = new Color((player.hasBullets > 0) ? 1 : 0,(player.hasBullets > 0) ? 1 : 0, (player.hasBullets > 0) ? 1 : 0, player.hasMusket ? 0.95f : 0);
        BulletCount.color = new Color(1, 1, 1, (player.hasBullets > 0) ? 0.95f : 0);
        Gun0Img.color = new Color(1, 1, 1, !player.hasMusket ? 0.3f : 0);
        Gun1Img.color = new Color(1, 1, 1, (player.hasGunIngres[0] && !player.hasMusket) ? 1 : 0);
        Gun2Img.color = new Color(1, 1, 1, (player.hasGunIngres[1] && !player.hasMusket) ? 1 : 0);
        Gun3Img.color = new Color(1, 1, 1, (player.hasGunIngres[2] && !player.hasMusket) ? 1 : 0);
        Gun4Img.color = new Color(1, 1, 1, (player.hasGunIngres[3] && !player.hasMusket) ? 1 : 0);
        Gun3Text.color = new Color(1, 1, 1, (player.hasGunIngres[2] && !player.hasMusket) ? 1 : 0);

        PaperAllImg.color = new Color(1, 1, 1, player.hasAllPapers ? 1 : 0);
        Paper1Img.color = new Color(1, 1, 1, (player.hasPapers[0] && !player.hasAllPapers) ? 0.6f : 0);
        Paper2Img.color = new Color(1, 1, 1, (player.hasPapers[1] && !player.hasAllPapers) ? 0.6f : 0);
        Paper3Img.color = new Color(1, 1, 1, (player.hasPapers[2] && !player.hasAllPapers) ? 0.6f : 0);

        if(player.hasItems[0] && Input.GetButtonDown("Swap1")) {
            ItemText.text = "Key";
            StartCoroutine("FadeOut");
        }
        if(player.hasItems[1] && Input.GetButtonDown("Swap2")) {
            ItemText.text = "Bread";
            StartCoroutine("FadeOut");
        }
        if(player.hasItems[2] && Input.GetButtonDown("Swap3")) {
            ItemText.text = "Potion";
            StartCoroutine("FadeOut");
        }
        if(player.hasItems[3] && Input.GetButtonDown("Swap4")) {
            ItemText.text = "Firecracker";
            StartCoroutine("FadeOut");
        }
        if(player.hasItems[4] && Input.GetButtonDown("Swap5")) {
            ItemText.text = "Poison";
            StartCoroutine("FadeOut");
        }
        if(player.hasMusket && Input.GetButtonDown("SwapQ")) {
            ItemText.text = "Musket";
            StartCoroutine("FadeOut");
        }
    }

    public IEnumerator FadeOut()
    {
        ItemText.color = new Color(1, 1, 1, 1);
        while(ItemText.color.a > 0.0f)
        {
            ItemText.color = new Color(1, 1, 1, ItemText.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
    }
}
