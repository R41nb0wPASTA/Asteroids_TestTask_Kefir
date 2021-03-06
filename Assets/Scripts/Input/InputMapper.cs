//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Inputs/InputMapper.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMapper : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMapper()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMapper"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5123b28f-fc58-4cce-a059-630bc7581e6f"",
            ""actions"": [
                {
                    ""name"": ""GoForward"",
                    ""type"": ""Button"",
                    ""id"": ""e5ab3d25-a377-4732-bb3f-c5906efdea98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1.401298E-45,pressPoint=1.401298E-45)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TurnRight"",
                    ""type"": ""Button"",
                    ""id"": ""0c64a137-f312-4f29-b9f3-03fd638c2bba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TurnLeft"",
                    ""type"": ""Button"",
                    ""id"": ""0a7778d4-9471-4c17-a7a5-a2977fb61969"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootRegular"",
                    ""type"": ""Button"",
                    ""id"": ""00990156-dc24-47e0-81b8-a3c2bf194071"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootLaser"",
                    ""type"": ""Button"",
                    ""id"": ""dd855694-13ba-4217-9d27-5bb46fc76bc5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""556ea866-cd8a-41b2-8a9a-d301ca5b15eb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""GoForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0bd0a1c-76dc-475f-b744-d69de961857a"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GoForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4970ad8-434d-492a-a485-ba2f37616c71"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GoForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2c6ced4-90e1-4604-a7e6-006b62c7561f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""GoForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cd5a27f-e0c4-444f-aec1-238c3e0fa411"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e95b1ec-8bbe-4825-b1a6-6d2dc4ed18bf"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""770f6c1c-7823-41ec-95e3-928d516ac31b"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9363221f-26b9-4144-b563-a444a024d5ed"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TurnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaccdbf8-f649-4e78-b09c-7c7c87e2a7cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbb5d84b-40e1-4240-b072-2bd4d692751f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f280f7-e2e1-4a55-ae54-aaac5d15f0f8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb8784a5-d6b4-48dc-927a-2f1169ad483e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""TurnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7335d6ac-df03-4e0a-bb99-4bef4536d9ec"",
                    ""path"": ""<Keyboard>/period"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootRegular"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7671831-40bc-48b1-b40f-765b09c39c15"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ShootRegular"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97f2537a-517c-4cf3-aee1-600d2321f501"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootRegular"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b1ba97c-1b6b-4646-a743-c5625fa7f285"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootLaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f36b0f1c-6603-48d8-9089-5873a61e7f56"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ShootLaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7be9f3a-4194-48d9-a3e8-9872efe3ec14"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShootLaser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""f801fffb-5874-4cdd-beab-72f97978bd58"",
            ""actions"": [
                {
                    ""name"": ""StartGame"",
                    ""type"": ""Button"",
                    ""id"": ""7846df74-6d5e-4057-9e30-a81228fa2c4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a6ebf0b-feaa-4616-a395-d05965306ab5"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bb4d74b-39d3-435b-b76f-5254e57d735d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_GoForward = m_Player.FindAction("GoForward", throwIfNotFound: true);
        m_Player_TurnRight = m_Player.FindAction("TurnRight", throwIfNotFound: true);
        m_Player_TurnLeft = m_Player.FindAction("TurnLeft", throwIfNotFound: true);
        m_Player_ShootRegular = m_Player.FindAction("ShootRegular", throwIfNotFound: true);
        m_Player_ShootLaser = m_Player.FindAction("ShootLaser", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_StartGame = m_Menu.FindAction("StartGame", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_GoForward;
    private readonly InputAction m_Player_TurnRight;
    private readonly InputAction m_Player_TurnLeft;
    private readonly InputAction m_Player_ShootRegular;
    private readonly InputAction m_Player_ShootLaser;
    public struct PlayerActions
    {
        private @InputMapper m_Wrapper;
        public PlayerActions(@InputMapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @GoForward => m_Wrapper.m_Player_GoForward;
        public InputAction @TurnRight => m_Wrapper.m_Player_TurnRight;
        public InputAction @TurnLeft => m_Wrapper.m_Player_TurnLeft;
        public InputAction @ShootRegular => m_Wrapper.m_Player_ShootRegular;
        public InputAction @ShootLaser => m_Wrapper.m_Player_ShootLaser;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @GoForward.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoForward;
                @GoForward.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoForward;
                @GoForward.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGoForward;
                @TurnRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnRight;
                @TurnRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnRight;
                @TurnRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnRight;
                @TurnLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnLeft;
                @TurnLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurnLeft;
                @ShootRegular.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootRegular;
                @ShootRegular.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootRegular;
                @ShootRegular.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootRegular;
                @ShootLaser.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLaser;
                @ShootLaser.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLaser;
                @ShootLaser.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootLaser;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GoForward.started += instance.OnGoForward;
                @GoForward.performed += instance.OnGoForward;
                @GoForward.canceled += instance.OnGoForward;
                @TurnRight.started += instance.OnTurnRight;
                @TurnRight.performed += instance.OnTurnRight;
                @TurnRight.canceled += instance.OnTurnRight;
                @TurnLeft.started += instance.OnTurnLeft;
                @TurnLeft.performed += instance.OnTurnLeft;
                @TurnLeft.canceled += instance.OnTurnLeft;
                @ShootRegular.started += instance.OnShootRegular;
                @ShootRegular.performed += instance.OnShootRegular;
                @ShootRegular.canceled += instance.OnShootRegular;
                @ShootLaser.started += instance.OnShootLaser;
                @ShootLaser.performed += instance.OnShootLaser;
                @ShootLaser.canceled += instance.OnShootLaser;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_StartGame;
    public struct MenuActions
    {
        private @InputMapper m_Wrapper;
        public MenuActions(@InputMapper wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartGame => m_Wrapper.m_Menu_StartGame;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @StartGame.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
                @StartGame.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
                @StartGame.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnStartGame;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartGame.started += instance.OnStartGame;
                @StartGame.performed += instance.OnStartGame;
                @StartGame.canceled += instance.OnStartGame;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnGoForward(InputAction.CallbackContext context);
        void OnTurnRight(InputAction.CallbackContext context);
        void OnTurnLeft(InputAction.CallbackContext context);
        void OnShootRegular(InputAction.CallbackContext context);
        void OnShootLaser(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnStartGame(InputAction.CallbackContext context);
    }
}
