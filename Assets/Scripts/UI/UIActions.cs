using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    public Button createAccountButton;
    public Button getAccountButton;
    public Button getAllAccountsButton;

    public TMP_InputField nameInput;
    public TMP_InputField emailInput;

    public TMP_InputField accountQueryInput;

    //public ClientController clientController;
    public static UIActions Instance { get; private set; }

    Database _db;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public void SetClientController(ClientController controller)
    //{
    //    clientController = controller;
    //}

    void Start()
    {
        _db = Database.singleton;

        createAccountButton.onClick.AddListener(() => {
            //Here we will use the ClientController instead of the db directly

            //clientController.ClientAddAccount(nameInput.text, emailInput.text);

            nameInput.text = "";
            emailInput.text = "";
        });

        getAccountButton.onClick.AddListener(() => {
            var account = _db.GetAccount(accountQueryInput.text);
            if (account != null)
            {
                Logger.Instance.Log($"Account: {account.name}, {account.email}, {account.created}");
            }
            else
            {
                Logger.Instance.LogError($"Account not found: {accountQueryInput.text}");
            }

            accountQueryInput.text = "";
        });

        getAllAccountsButton.onClick.AddListener(() => {
            var accounts = _db.GetAllAccounts();
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    Logger.Instance.Log($"Account: {account.name}, {account.email}, {account.created}");
                }
            }
            else
            {
                Logger.Instance.LogError($"No accounts found");
            }
        });
    }
}