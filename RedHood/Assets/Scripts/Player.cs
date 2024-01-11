using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;     // 말 그대로 player 속도
    public GameObject hangGun;  // 얘가 총을 매는 자세
    public GameObject musket;   // 머스킷 오브젝트
    public bool hasMusket;  // 얘가 총을 가졌나요?
    public int hasBullets = 0;  // 가진 총알 개수
    public bool[] hasGunIngres;     // 모은 총 부품 0 = 총신, 1 = 화약, 2 = 설계도
    public GameObject[] items;  // 앞으로 보여줄 아이템들
    public GameObject[] throws; // 앞으로 보여줄 투사체들
    public bool[] hasItems;     // 내가 이 아이템을 가졌나요?
    public bool hasAllPapers;   // 이거 true일 때 paper 다 모은 엔딩 
    public bool[] hasPapers;    // 내가 이 종이를 가졌나요?
    public Camera followCamera; // 카메라 받아옴
    public Text bulletCountText;   // 총알 개수 보여주는 텍스트
    public Text getText;    // 중앙에 뜨는 텍스트 획득 등..

    float hAxis;    // 이동 변수
    float vAxis;

    public bool wDown;     // 해당 키 눌렀나요?
    bool jDown;
    bool fDown;
    bool fUp;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    bool sDown5;
    bool sDownQ;

    bool isSwap;    // 얘가 아이템 스왑중인 상태인가요?
    bool isFireReady = true;    // 지금 총을 쏠 준비가 됐나요?
    bool isBorder;  // 얘가 벽에 맞닿았나요?
    public bool isTutorial;
    public bool isFlying = false;

    public bool isDead;
    public bool isBlockedFdown = false;

    public AudioClip locked_door_open;
    public AudioClip gunshot_sound;
    public AudioClip throw_sound;
    public AudioClip pickup_item;
    public AudioClip pickup_paper;
    public AudioClip speedup_sound;
    public AudioClip speeddown_sound;
    public AudioClip poison_break_sound;
    public AudioClip poison_active_sound;

    AudioSource sound_manager;

    public Vector3 moveVec;    // 이동 벡터

    Rigidbody rigid;    // 얘 rigid
    Animator anim;      // 얘 아니메   

    GameObject nearObject;  // 근처의 오브젝트
    public GameObject equipItem;    // 착용중인 아이템 
    float fireDelay;    // 총 공격속도
    int equipItemIndex = -1;    // 얘가 착용중인 아이템 코드

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        sound_manager = gameObject.AddComponent<AudioSource>();
        sound_manager.volume = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Move();
        if(isDead==false){
        GetInput();
        }
        Turn();
        UseItem();
        Attack();
        Swap();
        Interaction();
    }

    void GetInput() // Input 함수, 걍 다 getkeydown으로 바꿀까?
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        fDown = Input.GetButton("Fire2");
        if (isBlockedFdown == false)
        {
            fUp = Input.GetButtonDown("Fire1");
        }
        iDown = Input.GetButtonDown("Interaction");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        sDown4 = Input.GetButtonDown("Swap4");
        sDown5 = Input.GetButtonDown("Swap5");
        sDownQ = Input.GetButtonDown("SwapQ");
    }

    void Move() // 이동
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (!isFireReady || fDown) moveVec = Vector3.zero;   // 쏠 때, 조준할 때 멈추게 함

        if (!isBorder)   // 이동하는 거, 벽에 맞닿았을 때 제외
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero); // 이하 애니메이션
        //anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        //마우스 회전
        if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
        //키보드 회전
        transform.LookAt(transform.position + moveVec);
    }

    void UseItem()      // 아이템 사용
    {
        int throwIndex = -1;
        bool isThrow = false, isUse = false;
        if (!(0 <= equipItemIndex && equipItemIndex <= 4) || equipItem == null) return;
        Item item = equipItem.GetComponent<Item>();
        if (fUp)
        {      // 마우스 좌클릭 시 아이템 사용
            switch (equipItemIndex)
            {
                case 0: // 열쇠 사용
                    Debug.Log("use key");
                    if (nearObject == null) break;
                    if (nearObject.tag == "Locked")
                    {
                        sound_manager.clip = locked_door_open;
                        sound_manager.Play();
                        Destroy(nearObject);
                        getText.text = "";
                        isUse = true;
                    }
                    break;
                case 1: // 빵 사용
                    Debug.Log("use bread");
                    throwIndex = 0;
                    sound_manager.clip = throw_sound;
                    sound_manager.Play();
                    isThrow = true;
                    isUse = true;
                    break;
                case 2: // 물약 사용
                    Debug.Log("use potion");
                    StartCoroutine("UsePotion");
                    sound_manager.clip = speedup_sound;
                    sound_manager.Play();
                    isUse = true;
                    break;
                case 3: // 신호탄 사용
                    Debug.Log("use firecracker");
                    throwIndex = 1;
                    sound_manager.clip = throw_sound;
                    sound_manager.Play();
                    isThrow = true;
                    isUse = true;
                    break;
                case 4: // 독 사용
                    Debug.Log("use poison");
                    throwIndex = 2;
                    sound_manager.clip = throw_sound;
                    sound_manager.Play();
                    isThrow = true;
                    isUse = true;
                    break;
            }
            if (isThrow)
            {   // 빵, 신호탄, 독이 투척용 물건이고 얘네만 코루틴 발동
                StartCoroutine(StopThrow(throwIndex));
            }
            if (isUse)
            {     //정상적으로 사용했을 시 사용한 아이템 없애기
                hasItems[equipItemIndex] = false;
                equipItem.SetActive(false);
                equipItem = null;
                equipItemIndex = -1;
            }

        }
    }

    IEnumerator UsePotion() // 포션 이용할 때 나오는 코루틴, 플레이어가 5초간 1.5배 빨라짐
    {
        if (isTutorial == false)
        {
            float temp = speed;
            speed *= 1.5f;
            yield return new WaitForSeconds(5f);
            speed = temp;
            sound_manager.clip = speeddown_sound;
            sound_manager.Play();
            yield return null;
        }
        else
        {
            float temp = speed;
            speed *= 1.5f;
            yield return new WaitForSeconds(5f);
            sound_manager.clip = speeddown_sound;
            sound_manager.Play();
            yield return null;
        }
    }

    IEnumerator StopThrow(int throwIndex)   // 투척 아이템을 던지고 그 물건이 멈추는 함수
    {

        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);   // 투사체 던져지는거 구현
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            Vector3 nextVec = rayHit.point - transform.position;
            nextVec.y = 2.5f;
            GameObject instantThrow = Instantiate(throws[throwIndex], transform.position, transform.rotation);
            SphereCollider throwSpCol = instantThrow.GetComponent<SphereCollider>();
            Item getThrowItem = instantThrow.GetComponent<Item>();

            Rigidbody rigidThrow = instantThrow.GetComponent<Rigidbody>();
            rigidThrow.isKinematic = false;
            rigidThrow.AddForce(nextVec, ForceMode.Impulse);
            rigidThrow.AddTorque(Vector3.back * 10, ForceMode.Impulse);
            throwSpCol.enabled = false;
            isFlying = true;
            yield return new WaitForSeconds(1f);    // 투사체가 마우스 클릭한 위치까지만 가고 멈추게 함

            isFlying = false;
            rigidThrow.velocity = Vector3.zero;
            rigidThrow.angularVelocity = Vector3.zero;
            rigidThrow.isKinematic = true;
            throwSpCol.enabled = true;
            if (throwIndex == 1)
            {   // 오브젝트 : 신호탄이 던져지고 폭발
                if (!isTutorial)
                    getThrowItem.FirecrackerUse();
                else
                {
                    getThrowItem.FirecrackerUseTuto();
                }

                Destroy(instantThrow, 10f);
            }
            if (throwIndex == 2)
            {   // 오브젝트 : 독이 던져지고 멈출 때 효과 발동하게 함 이후 충돌했을 때는 Item.cs에
                sound_manager.clip = poison_break_sound;
                sound_manager.Play();
                Invoke("Poison_active_sound_play", 1f);
                Destroy(instantThrow, 10f);
            }
            // 멈추고 10초 뒤에 사라짐

        }
        yield return null;
    }

    void Poison_active_sound_play()
    {
        AudioSource manager2 = gameObject.AddComponent<AudioSource>();
        manager2.volume = 0.4f;
        manager2.clip = poison_active_sound;
        manager2.Play();
    }

    void Attack()   // 총 쏠 때
    {
        if (equipItemIndex != 100 || equipItem == null) return;  // 무기 착용할 때 아니면 함수 재낌
        Item item = equipItem.GetComponent<Item>(); // 착용한 아이템 함수 갖고오고
        fireDelay += Time.deltaTime;    // 공격속도 구현
        isFireReady = 0.5f < fireDelay;

        if (fUp && isFireReady && !isSwap && hasBullets > 0)
        {   // 총 쏠때 조건들
            fireDelay = 0;
            moveVec = Vector3.zero;
            item.WUse();    // 총 쏘고 나오는 총알 구현
            anim.SetTrigger("doShot");  // 총 쏠 때 애니메이션
            hasBullets--;
            sound_manager.clip = gunshot_sound;
            sound_manager.Play();
            bulletCountText.text = hasBullets.ToString();   // 남은 탄환 보여주는 거 
        }

    }

    void Swap()    // 1~5, q 누르면 착용한 아이템이 바뀌는 거 
    {
        if (sDown1 && (!hasItems[0] || equipItemIndex == 0))
            return;
        if (sDown2 && (!hasItems[1] || equipItemIndex == 1))
            return;
        if (sDown3 && (!hasItems[2] || equipItemIndex == 2))
            return;
        if (sDown4 && (!hasItems[3] || equipItemIndex == 3))
            return;
        if (sDown5 && (!hasItems[4] || equipItemIndex == 4))
            return;
        if (sDownQ && (!hasMusket || equipItemIndex == 100))
            return;

        int itemIndex = -1;     // 지금 갖고있는 아이템 코드가 몇 번인지?
        if (sDown1) itemIndex = 0;   // 열쇠
        if (sDown2) itemIndex = 1;   // 빵
        if (sDown3) itemIndex = 2;   // 물약
        if (sDown4) itemIndex = 3;   // 신호탄
        if (sDown5) itemIndex = 4;   // 독
        if (sDownQ) itemIndex = 100; // 총

        if (sDown1 || sDown2 || sDown3 || sDown4 || sDown5)
        {    // 아이템 키를 눌렀을 때 
            if (equipItem != null)   // 원래 손에 있던 아이템 바꾸기
                equipItem.SetActive(false);
            if (hasMusket)   // 총이 있다면 메는 자세로 바꾸고
                hangGun.SetActive(true);

            equipItemIndex = itemIndex; // 착용했던 아이템 코드에 따른 아이템을 쥠
            equipItem = items[itemIndex];
            equipItem.SetActive(true);

            anim.SetTrigger("doSwap");  // 바꾸는 모션

            isSwap = true;  // 바꾸는 중을 나타냄

            Invoke("SwapOut", 0.4f);    // 바꾸는 딜레이 0.4초
        }

        if (sDownQ)
        {    // 총 바꿀 때, 아이템 바꿀 때랑 비슷함
            if (equipItem != null)
                equipItem.SetActive(false);
            hangGun.SetActive(false);   // 메는 자세를 없애고 들어야지

            equipItem = musket;     //총 드는 걸로 바꿈
            equipItemIndex = itemIndex;
            equipItem.SetActive(true);

            anim.SetTrigger("doSwap");   // 얜 나중에 모션 추가해야하지 않을까?

            isSwap = true;  // 얘도 똑같이 0.4초 걸림, 딜레이 좀 더 늘릴까

            Invoke("SwapOut", 0.4f);
        }
    }

    void SwapOut()  // 바꾸는 자세가 끝났다면?
    {
        isSwap = false;
    }

    void Interaction()  // 상호작용 키 누를 때, e 키로 
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Item")
            {      // 소모용 아이템을 먹었을 때 (1~5)
                Item item = nearObject.GetComponent<Item>();
                int itemIndex = item.value;
                if (item.value == 5)
                {   // 튜토 신호탄이라면
                    itemIndex = 3;
                    isTutorial = true;
                }
                if (hasItems[itemIndex] == true)
                {
                    getText.text = "이미 갖고있다..";
                    StartCoroutine("AlreadyHave", nearObject);
                    return;
                }
                else
                {
                    sound_manager.clip = pickup_item;
                    sound_manager.Play();
                }
                hasItems[itemIndex] = true; // 이 아이템을 내가 갖고 있습니다

                Destroy(nearObject);    // 땅에 있던 아이템 없애고
                getText.text = "";  // 획득하라는 문구도 없앤다
            }

            if (nearObject.tag == "Ingredient")
            {       // 총 부품을 먹었을 때
                Item item = nearObject.GetComponent<Item>();
                int ingIndex = item.value;
                if (hasGunIngres[ingIndex] == true)
                {
                    getText.text = "이미 갖고있다..";
                    StartCoroutine("AlreadyHave", nearObject);
                    return;
                }
                else
                {
                    sound_manager.clip = pickup_item;
                    sound_manager.Play();
                }
                hasGunIngres[ingIndex] = true;

                Destroy(nearObject);
                getText.text = "";

                if (hasGunIngres[0] && hasGunIngres[1] && hasGunIngres[2])
                {     // 총 부품 다 먹었으면 
                    hasMusket = true;   // 난 총을 가졌어요
                    hangGun.SetActive(true);    // 총 가졌으니 총 맬게요
                    //[0] = gunbarrel, [1] = gunpowder, [2] = blueprint
                }
                if (ingIndex == 3) hasBullets++;   // 총알 먹었으면 총알 개수 올라감
                if (hasMusket) bulletCountText.text = hasBullets.ToString();   // 현재 총알 개수 갱신
            }

            if (nearObject.tag == "Paper")
            {     // 종이를 먹었을 때 
                Item item = nearObject.GetComponent<Item>();
                int paperIndex = item.value;
                if (hasPapers[paperIndex] == true)
                {
                    getText.text = "이미 갖고있다..";
                    StartCoroutine("AlreadyHave", nearObject);
                    return;
                }
                else
                {
                    sound_manager.clip = pickup_paper;
                    sound_manager.Play();
                }
                hasPapers[paperIndex] = true;

                Destroy(nearObject);
                getText.text = "";

                if (hasPapers[0] && hasPapers[1] && hasPapers[2])
                    hasAllPapers = true;    // 종이 다 먹었다는 이벤트 활성화
            }
        }
    }

    IEnumerator AlreadyHave(GameObject haveObject)
    {
        SphereCollider haveCollider = haveObject.GetComponent<SphereCollider>();
        haveCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        haveCollider.enabled = true;
    }

    void FreezeRotation()   // 부딪히고 player가 관성?때매 안돌아가게 함
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall() // Layer가 Wall인 곳을 못 통과하게 함
    {
        Debug.DrawRay(transform.position, transform.forward * 3, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 3, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate() // 특정상황 발생 시 Update
    {
        FreezeRotation();
        StopToWall();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ingredient" || other.tag == "Paper" || other.tag == "Locked")
        {   // 주위에 오브젝트가 있네?
            nearObject = other.gameObject;
            if (other.tag != "Locked")   // 오브젝트들 획득 문구 뜨게 함
                getText.text = other.name.ToString() + " (E) 획득";
            else    // 잠긴 문을 발견했을 때 문구
                getText.text = "문이 잠겨있다. 열쇠로 열 수 있을 것 같다..";
        }
        /*switch(other.tag) {
        case "Item":
            break;
        case "Ingredient":
            break;
        case "Paper":
            break;
        }*/
        //Debug.Log(other.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item" || other.tag == "Ingredient" || other.tag == "Paper")
            nearObject = null;  // 옵젝들에게서 멀어졌을 때
        if (other.tag != "Projectile")   // 투사체는 적용 x
            getText.text = "";
    }
}

