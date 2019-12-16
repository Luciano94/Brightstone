using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance{
        get{
            instance = FindObjectOfType<SoundManager>();
            if(instance == null){
                GameObject go = new GameObject("Sound Manager");
                instance = go.AddComponent<SoundManager>();
            }
            return instance;
        }
    }

    private void Awake(){
        DontDestroyOnLoad(gameObject);
        GameObject go = Instantiate(Resources.Load ("SoundBankLoader")) as GameObject;
    }
    
    public void StopSounds(){
        AkSoundEngine.StopAll();
    }


    /* Base Wwise event function, to copy-paste, do not use as is
    public void Foo(){ 
        AkSoundEngine.PostEvent("", gameObject);
    }
    */

    //SFX
    //Old player attacks, deprecated
    public void PlayerAttackLight(){ //El jugador usa un ataque ligero
        AkSoundEngine.PostEvent("PlayerAttackLight", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackLightHit(){ //El ataque ligero de jugador golpea a un enemigo
        AkSoundEngine.PostEvent("PlayerAttackLightHit", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackLight2(){ //El jugador usa un ataque ligero
        AkSoundEngine.PostEvent("PlayerAttackLight2", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackHeavy(){ //El jugador usa un ataque pesado
        AkSoundEngine.PostEvent("PlayerAttackHeavy", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackHeavyHit(){ //El ataque pesado del jugador golpea a un enemigo
        //AkSoundEngine.PostEvent("PlayerAttackHeavyHit", GameManager.Instance.playerSts.gameObject);
    }

    //new player attack sounds
    public void PlayerAttackA(){ //El jugador realiza el ataque correspondiente al botón "A"
        AkSoundEngine.PostEvent("PlayerAttackA", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackAx7(){ //El jugador completó el combo de 7 ataques seguidos de "A" satisfactoriamente
        AkSoundEngine.PostEvent("PlayerAttackAx7", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackX(){ //El jugador realiza el ataque correspondiente al botón "X"
        AkSoundEngine.PostEvent("PlayerAttackX", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackXx5(){ //El jugador completó el combo de 5 ataques seguidos de "X" satisfactoriamente
        AkSoundEngine.PostEvent("PlayerAttackXx5", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackY(){ //El jugador realiza el ataque correspondiente al botón "Y"
        AkSoundEngine.PostEvent("PlayerAttackY", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackYx5(){ //El jugador completó el combo de 5 ataques seguidos de "Y" satisfactoriamente
        AkSoundEngine.PostEvent("PlayerAttackYx5", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackB(){ //El jugador realiza el ataque correspondiente al botón "B"
        AkSoundEngine.PostEvent("PlayerAttackB", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboA1(){ //El jugador realiza el 1er ataque rojo del combo XXAAAA
        AkSoundEngine.PostEvent("PlayerComboA1", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboA2(){ //El jugador realiza el 2do ataque rojo del combo XXAAAA
        AkSoundEngine.PostEvent("PlayerComboA2", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboA3(){ //El jugador realiza el 3er ataque rojo del combo XXAAAA
        AkSoundEngine.PostEvent("PlayerComboA3", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboA4(){ //El jugador realiza el 4to y último ataque rojo del combo XXAAAA
        AkSoundEngine.PostEvent("PlayerComboA4", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboB1(){ //El jugador realiza el 1er ataque rojo del combo AAAXXXXX
        AkSoundEngine.PostEvent("PlayerComboB1", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboB2(){ //El jugador realiza el 2do ataque rojo del combo AAAXXXXX
        AkSoundEngine.PostEvent("PlayerComboB2", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboB3(){ //El jugador realiza el 3er ataque rojo del combo AAAXXXXX
        AkSoundEngine.PostEvent("PlayerComboB3", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboB4(){ //El jugador realiza el 4to ataque rojo del combo AAAXXXXX
        AkSoundEngine.PostEvent("PlayerComboB4", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerComboB5(){ //El jugador realiza el 5to ataque rojo del combo AAAXXXXX
        AkSoundEngine.PostEvent("PlayerComboB5", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerShurikenHit(GameObject enemy){ //El jugador golpea un enemigo con un shuriken
        AkSoundEngine.PostEvent("PlayerShurikenHit", enemy);
    }
    public void PlayerGetsSoul(){ //El jugador absorbe un cúmulo de esencia de Brightstone
        AkSoundEngine.PostEvent("PlayerGetsSoul", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerXP(float porcCompleted){ //Hay un cambio en el porcentaje de XP del jugador
        AkSoundEngine.SetRTPCValue("PlayerXP", porcCompleted * 100);
    }
    public void PlayerLvlUp(){ //La barra de experiencia de nivel se completa
        AkSoundEngine.PostEvent("PlayerLvlUp", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerDamaged(){ //El jugador recibe daño
        AkSoundEngine.PostEvent("PlayerDamaged", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerDeath(){ //El jugador muere
        AkSoundEngine.PostEvent("PlayerDeath", GameManager.Instance.playerSts.gameObject);
    }
    public void PostMortem(){ //El cuerpo del jugador se deshace
        AkSoundEngine.PostEvent("PostMortem", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerRespawn(){ //El jugador revive
        AkSoundEngine.PostEvent("PlayerRespawn", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerParry(){ //El jugador ejecuta un parry
        //AkSoundEngine.PostEvent("PlayerParry", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerParryHit(){ //El parry del jugador bloquea un ataque
        AkSoundEngine.PostEvent("PlayerParryHit", GameManager.Instance.playerSts.gameObject);
    }
    public void EnemyMeleeSwordAttack(GameObject enemy){ //El melee enemigo ataca con espada
        AkSoundEngine.PostEvent("EnemyMeleeSwordAttack", enemy);   
    }
    public void EnemyMeleeSwordAttackHit(GameObject enemy){ //El ataque con espada del melee enemigo golpea al jugador
        //AkSoundEngine.PostEvent("EnemyMeleeSwordAttackHit", enemy);
    }
    public void EnemyMeleeDeath(GameObject enemy){ //Muere un melee enemigo
        //AkSoundEngine.PostEvent("EnemyMeleeDeath", enemy);
    }
    public void EnemyArcherTightBow(GameObject enemyArcher){ //El arquero enemigo tensa el arco
        AkSoundEngine.PostEvent("EnemyArcherTightBow", enemyArcher);
    }
    public void EnemyArcherReleaseBow(GameObject enemyArcher){ //El arquero enemigo suelta el arco
        AkSoundEngine.PostEvent("EnemyArcherReleaseBow", enemyArcher);
    }
    public void EnemyArcherDeath(GameObject enemyArcher){ //Muere un arquero enemigo
        //AkSoundEngine.PostEvent("EnemyArcherDeath", enemyArcher);
    }
    public void BossSwordAttack(GameObject boss){ //El boss ataca con espada
        AkSoundEngine.PostEvent("BossSwordAttack", boss);
    }
    public void BossSwordAttackHit(GameObject boss){ //El ataque con espada del boss golpea al jugador
        //AkSoundEngine.PostEvent("BossSwordAttackHit", boss);
    }
    public void BossDeath(GameObject boss){ //Muere un boss
        LevelClear();
        AkSoundEngine.PostEvent("BossDeath", boss);
    }
    public void iBossDeath(GameObject boss){ //El boss intermedio muere
        AkSoundEngine.PostEvent("BossDeath", boss);
    }
    public void ArrowHit(GameObject arrow){ //Una flecha golpea a alguien
        AkSoundEngine.PostEvent("ArrowHit", arrow);
    }
    public void RoomNewEnter(){ //Se entra a una habitación nueva
        AkSoundEngine.PostEvent("RoomNewEnter", gameObject);
    }
    public void RoomClear(){ //No quedan más enemigos en la habitacion
        /*Necesito que cuando el jugador termina el tutorial, se gatille este evento 
        (cuando se va del room o, preferentemente, antes)*/
        AkSoundEngine.PostEvent("RoomClear", gameObject);
    }
    public void RoomBossEnter(){ //Se entra a un boss room
        AkSoundEngine.PostEvent("RoomBossEnter", gameObject);
    }
    public void RoomiBossEnter(){ //El jugador entra al cuarto del boss intermedio
        AkSoundEngine.PostEvent("RoomBossEnter", gameObject);
    }
    public void LevelEnter(){ //Inicio de un nivel
        AkSoundEngine.PostEvent("LevelEnter", gameObject);
    }
    public void LevelClear(){ //Se termina el nivel
        //AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("LevelClear", gameObject);
    }
    public void PlayerHP(float playerHPPercentage){ //Hay un cambio en el porcentaje de vida del jugador
        AkSoundEngine.SetRTPCValue("PlayerHP", playerHPPercentage * 100);
    }
    public void BossHP(float bossHPPercentage){ //Hay un cambio en el porcentaje de vida de un boss
        AkSoundEngine.SetRTPCValue("BossHP", bossHPPercentage * 100);
    }
    public void MenuItemHover(){ //Se hace hover sobre un item de menu
        AkSoundEngine.PostEvent("MenuItemHover", gameObject);
    }
    public void MenuItemClick(){ //Se hace click sobre un item de menu
        AkSoundEngine.PostEvent("MenuItemClick", gameObject);
    }
    public void MenuOpen(){ //Se abre la escena de menú principal
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("MenuOpen", gameObject);
    }
    public void PlayerDash(){ //El jugador ejecuta el Dash
        AkSoundEngine.PostEvent("PlayerDash", GameManager.Instance.playerSts.gameObject);
    }
    public void SelectAttX(){ //El jugador selecctiona el ataque Beatdown
        AkSoundEngine.PostEvent("SelectAttX", gameObject);
    }
    public void SelectAttA(){ //El jugador selecctiona el ataque Zone
        AkSoundEngine.PostEvent("SelectAttA", gameObject);
    }
    public void SelectAttSame(){ //El jugador selecctiona el ataque ya seleccionado
        AkSoundEngine.PostEvent("SelectAttSame", gameObject);
    }
    public void SynopsisOpen(){ //Se abre la escena de menú principal
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("SynopsisOpen", gameObject);
    }
}