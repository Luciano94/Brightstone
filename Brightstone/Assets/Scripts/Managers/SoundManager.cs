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
       // SceneManager.sceneLoaded += OnLoadScene;
    }
    /*void OnLoadScene(Scene scene, LoadSceneMode mode){
        mainCamera = Camera.current.gameObject;
    }*/
    
    public void StopSounds(){
        AkSoundEngine.StopAll();
    }

    //SFX
    public void PlayerAttackLight(){ //El jugador usa un ataque ligero
        AkSoundEngine.PostEvent("PlayerAttackLight", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackLightHit(){ //El ataque ligero de jugador golpea a un enemigo
        //AkSoundEngine.PostEvent("PlayerAttackLightHit", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackHeavy(){ //El jugador usa un ataque pesado
        AkSoundEngine.PostEvent("PlayerAttackHeavy", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerAttackHeavyHit(){ //El ataque pesado del jugador golpea a un enemigo
        //AkSoundEngine.PostEvent("PlayerAttackHeavyHit", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerDamaged(){ //El jugador recibe daño
        //AkSoundEngine.PostEvent("PlayerDamaged", GameManager.Instance.playerSts.gameObject);
    }
    public void PlayerDeath(){ //El jugador muere
        LevelClear();
        AkSoundEngine.PostEvent("PlayerDeath", GameManager.Instance.playerSts.gameObject);
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
    public void ArrowHit(GameObject arrow){ //Una flecha golpea a alguien
        AkSoundEngine.PostEvent("ArrowHit", arrow);
    }
    public void RoomNewEnter(){ //Se entra a una habitación nueva
        AkSoundEngine.PostEvent("RoomNewEnter", gameObject);
    }
    public void RoomClear(){ //No quedan más enemigos en la habitacion
        AkSoundEngine.PostEvent("RoomClear", gameObject);
    }
    public void RoomBossEnter(){ //Se entra a un boss room
        AkSoundEngine.PostEvent("RoomBossEnter", gameObject);
    }
    public void LevelEnter(){ //Inicio de un nivel
        AkSoundEngine.PostEvent("LevelEnter", gameObject);
    }
    public void LevelClear(){ //Se termina el nivel
        AkSoundEngine.StopAll();
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
    public void SynopsisOpen(){ //Se abre la escena de menú principal
        AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("SynopsisOpen", gameObject);
    }

    // Music (turned out this were not events)
    /*public void MainMenu(){
        AkSoundEngine.PostEvent("Main Menu", gameObject);
    }
    public void Synopsis(){
        AkSoundEngine.PostEvent("Synopsis", gameObject);
    }
    public void Tutorial(){
        AkSoundEngine.PostEvent("Tutorial", gameObject);
    }
    public void Combat(){
        AkSoundEngine.PostEvent("Combat", gameObject);
    }
    public void Explore(){
        AkSoundEngine.PostEvent("Explore", gameObject);
    }
    public void Boss(){
        AkSoundEngine.PostEvent("Boss", gameObject);
    }
    public void Victory(){
        LevelClear();
        AkSoundEngine.PostEvent("Victory", gameObject);
    }
    public void Death(){
        LevelClear();
        AkSoundEngine.PostEvent("Death", gameObject);
    }*/
}