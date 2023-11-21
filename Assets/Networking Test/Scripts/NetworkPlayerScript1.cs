using UnityEngine;
using Mirror;
using QuickStart;

namespace NetworkingTest
{
    public class NetworkPlayerScript1 : NetworkBehaviour
    {
        public TextMesh playerNameText;
        public GameObject floatingInfo;

        private Material playerMaterialClone;

        private SceneScript sceneScript;

        [SyncVar(hook = nameof(OnNameChanged))]
        public string playerName;

        [SyncVar(hook = nameof(OnColourChanged))]
        public Color playerColor = Color.white;

        private int selectedWeaponLocal = 0;
        public GameObject[] weaponArray;

        [SyncVar(hook = nameof(OnWeaponChanged))]
        public int activeWeaponSynced = 0;

        private Weapon activeWeapon;
        private float weaponCooldownTime;

        void OnWeaponChanged(int _old, int _new)
        {
            // disable old weapon
            // in range and not null
            if (0 < _old && _old < weaponArray.Length && weaponArray[_old] != null)
                weaponArray[_old].SetActive(false);

            // enable new weapon
            // in range and not null
            if (0 < _new && _new < weaponArray.Length && weaponArray[_new] != null)
            {
                weaponArray[_new].SetActive(true);
                activeWeapon = weaponArray[activeWeaponSynced].GetComponent<Weapon>();
                if (isLocalPlayer)
                    sceneScript.UIAmmo(activeWeapon.weaponAmmo);
            }    
        }

        [Command]
        public void CmdChangeActiveWeapon(int newIndex)
        {
            activeWeaponSynced = newIndex;
        }


        private void Awake()
        {
            //allows all players to run this
            sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;

            //disable all weapons
            foreach (var item in weaponArray)
                if (item != null)
                {
                    item.SetActive(false);
                }

            if (selectedWeaponLocal < weaponArray.Length && weaponArray[selectedWeaponLocal] != null)
            {
                activeWeapon = weaponArray[selectedWeaponLocal].GetComponent<Weapon>();
                sceneScript.UIAmmo(activeWeapon.weaponAmmo);
            }

        }

        [Command]
        public void CmdSendPlayerMessage()
        {
            if (sceneScript)
            {
                sceneScript.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
            }
        }

        void OnNameChanged(string _old, string _new)
        {
            playerNameText.text = playerName;
        }

        void OnColourChanged(Color _old, Color _new)
        {
            playerNameText.color = _new;
            playerMaterialClone = new Material(GetComponent<Renderer>().material);
            playerMaterialClone.color = _new;
            GetComponent<Renderer>().material = playerMaterialClone;

        }

        public override void OnStartLocalPlayer()
        {
            sceneScript.playerScript = this;

            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, 0);

            floatingInfo.transform.localPosition = new Vector3(0, 1.33f, 0);
            floatingInfo.transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);

            string name = "Player" + Random.Range(100, 999);
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            CmdSetupPlayer(name, color);
        }

        [Command]
        public void CmdSetupPlayer(string _name, Color _col)
        {
            playerName = _name;
            playerColor = _col;
            sceneScript.statusText = $"{playerName} joined.";
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                floatingInfo.transform.LookAt(Camera.main.transform.position);
                return;
            }

            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
            float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

            transform.Rotate(0, moveX, 0);
            transform.Translate(0, 0, moveZ);

            if (Input.GetButtonDown("Fire2")) //Fire2 is mouse 2nd click and left alt
            {
                selectedWeaponLocal += 1;

                if (selectedWeaponLocal > weaponArray.Length)
                    selectedWeaponLocal = 1;

                CmdChangeActiveWeapon(selectedWeaponLocal);
            }

            if (Input.GetButtonDown("Fire1")) //Fire1 is mouse 1st click
            {
                if (activeWeapon && Time.time > weaponCooldownTime && activeWeapon.weaponAmmo > 0)
                {
                    weaponCooldownTime = Time.time + activeWeapon.weaponCooldown;
                    activeWeapon.weaponAmmo -= 1;
                    sceneScript.UIAmmo(activeWeapon.weaponAmmo);
                    CmdShootRay();
                }
            }
        }

        [Command]
        void CmdShootRay()
        {
            RpcFireWeapon();
        }

        [ClientRpc]
        void RpcFireWeapon()
        {
            //bulletAudio.Play(); muzzleflash  etc
            GameObject bullet = Instantiate(activeWeapon.weaponBullet, activeWeapon.weaponFirePosition.position, activeWeapon.weaponFirePosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * activeWeapon.weaponSpeed;
            Destroy(bullet, activeWeapon.weaponLife);
        }



    }

}
