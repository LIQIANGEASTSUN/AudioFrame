using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private int _instanceBgID = -1;
    // Update is called once per frame
    void Update()
    {
        SoundManager.GetInstance().Update();
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogError("Play");
            //SoundManager.GetInstance().PlayAudio2D("sound_actconfirm", true);

            SoundManager.GetInstance().PlayAudio3D(transform.position, "sound_actconfirm", false, 1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _instanceBgID = SoundManager.GetInstance().PlayBG("fuShnegJi", true, 1);
            //SoundManager.GetInstance().PauseBG();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.GetInstance().Pause(_instanceBgID);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.GetInstance().UnPause(_instanceBgID);
        }
    }
}
