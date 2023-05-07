using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : PauseMenu
{
        public GameObject optionsMenu;
        public bool isOppened;

        void Start()
        {
            optionsMenu.SetActive(false);
        }

        void Update()
        {
           if (Input.GetKeyDown(KeyCode.O))
            {
               if (isOppened)
                {
                    DeactivateOptions();
                
                }
               else
                {
                    ActivateOptions();
                
                }
            }
        }

        public void ActivateOptions()
        {
            optionsMenu.SetActive(true);
            isOppened = true;
            pauseMenu.SetActive(false);
        }

        public void DeactivateOptions()
        {
            optionsMenu.SetActive(false);
            isOppened = false;
            pauseMenu.SetActive(true);
        }

   
        public void GoBackButtonPressed()
        {
           DeactivateOptions();
        }

}
