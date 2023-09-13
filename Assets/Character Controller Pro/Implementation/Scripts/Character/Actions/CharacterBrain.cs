using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Lightbug.Utilities;
using Lightbug.CharacterControllerPro.Core;

namespace Lightbug.CharacterControllerPro.Implementation
{


    /// <summary>
    /// This class is responsable for detecting inputs and managing character actions.
    /// </summary>
    [AddComponentMenu("Character Controller Pro/Implementation/Character/Character Brain")]
    [DefaultExecutionOrder(int.MinValue)]
    public class CharacterBrain : MonoBehaviour
    {
        [SerializeField]
        bool isAI = false;

        [SerializeField]
        InputHandlerSettings inputHandlerSettings = new InputHandlerSettings();

        [SerializeField]
        CharacterAIBehaviour aiBehaviour = null;

        [SerializeField]
        CharacterActions characterActions = new CharacterActions();

        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        // ─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        
        CharacterAIBehaviour currentAIBehaviour = null;
        bool firstUpdateFlag = false;

        /// <summary>
        /// Gets the current brain mode (AI or Human).
        /// </summary>
        public bool IsAI => isAI;

        /// <summary>
        /// Gets the current actions values from the brain.
        /// </summary>
        public CharacterActions CharacterActions => characterActions;               

        /// <summary>
        /// Sets the internal CharacterActions value.
        /// </summary>
        public void SetAction(CharacterActions characterActions) => this.characterActions = characterActions;


        /// <summary>
        /// Sets the type of brain.
        /// </summary>
        public void SetBrainType(bool isAI)
        {
            characterActions.Reset();

            if (isAI)
                SetAIBehaviour(aiBehaviour);

            this.isAI = isAI;
        }

        /// <summary>
        /// Sets the AI behaviour type.
        /// </summary>
        public void SetAIBehaviour(CharacterAIBehaviour aiBehaviour)
        {
            if (aiBehaviour == null)
                return;

            currentAIBehaviour?.ExitBehaviour(Time.deltaTime);
            characterActions.Reset();
            currentAIBehaviour = aiBehaviour;
            currentAIBehaviour.EnterBehaviour(Time.deltaTime);
        }

        public void UpdateBrainValues(float dt)
        {
            if (Time.timeScale == 0)
                return;

            if (IsAI)
                UpdateAIBrainValues(dt);
            else
                UpdateHumanBrainValues(dt);
        }

        void UpdateHumanBrainValues(float dt)
        {
            characterActions.SetValues(inputHandlerSettings.InputHandler);
            characterActions.Update(dt);
        }

        void UpdateAIBrainValues(float dt)
        {
            currentAIBehaviour?.UpdateBehaviour(dt);
            characterActions.SetValues(currentAIBehaviour.characterActions);
            characterActions.Update(dt);
        }

        #region Unity's messages

        protected virtual void Awake()
        {
            characterActions.InitializeActions();
            inputHandlerSettings.Initialize(gameObject);
        }

        protected virtual void OnEnable()
        {
            characterActions.InitializeActions();
            characterActions.Reset();
        }


        protected virtual void OnDisable()
        {
            characterActions.Reset();
        }

        void Start()
        {
            SetBrainType(isAI);
        }

        protected virtual void FixedUpdate()
        {
            float dt = Time.deltaTime;
            UpdateBrainValues(0f);
            firstUpdateFlag = true;
        }

        protected virtual void Update()
        {
            float dt = Time.deltaTime;

            if (firstUpdateFlag)
            {
                firstUpdateFlag = false;
                characterActions.Reset();
            }

            UpdateBrainValues(dt);
        }


        #endregion


    }

}
