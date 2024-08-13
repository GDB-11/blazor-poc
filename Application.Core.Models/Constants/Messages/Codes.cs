namespace Application.Core.Models.Constants.Messages;

public static class Codes
{
    public static class UITextCodes
    {
        public const string Motto = "Motto";

        public static class RegisterCodes
        {
            public const string Title = "RegisterHeader";
            public const string UsernameLabel = "UsernameLabel";
            public const string UsernamePlaceholder = "UsernamePlaceholder";
            public const string PasswordLabel = "PasswordLabel";
            public const string EmailLabel = "EmailLabel";
            public const string EmailPlaceholder = "EmailPlaceholder";
            public const string TermsAndConditionsLabel = "TermsAndConditionsLabel";
            public const string CreateAccountButtonText = "CreateAccountButtonText";
            public const string AlreadyHaveAnAccountText = "AlreadyHaveAnAccountText";
            public const string LoginHereText = "LoginHereText";
        }

        public static class LogInCodes
        {
            public const string Title = "LogInHeader";
            public const string UsernameLabel = "UsernameLabel";
            public const string UsernamePlaceholder = "UsernamePlaceholder";
            public const string PasswordLabel = "PasswordLabel";
            public const string RememberMeLabel = "RememberMeLabel";
            public const string LogInButtonText = "LogInButtonText";
            public const string ForgotPasswordText = "ForgotPasswordText";
            public const string DontHaveAnAccountText = "DontHaveAnAccountText";
            public const string SignUpHereText = "SignUpHereText";
        }

        public static class SidebarCodes
        {
            public const string DashboardText = "DashboardText";
            public const string PortfoliosText = "PortfoliosText";
            public const string AssetText = "AssetText";
            public const string LogOutText = "LogOutText";
        }

        public static class PortfolioCodes
        {
            public const string AddPortfolioModalTitleText = "AddPortfolioModalTitleText";
            public const string NameTableHeaderText = "NameTableHeaderText";
            public const string ActionTableHeaderText = "ActionTableHeaderText";
            public const string PortfolioNameModalFormText = "PortfolioNameModalFormText";
            public const string PortfolioNameModalFormPlaceholderText = "PortfolioNameModalFormPlaceholderText";
            public const string AddPortfolioModalButtonText = "AddPortfolioModalButtonText";
            public const string EditPortfolioModalTitleText = "EditPortfolioModalTitleText";
            public const string EditPortfolioModalButtonText = "EditPortfolioModalButtonText";

            public const string PortfolioDeletionConfirmationBody = "IC-PO0001";
            public const string PortfolioDeletionConfirmationMessage = "IC-PO0004";
            public const string PortfolioDeletionCancellationMessage = "IC-PO0005";
        }

        public static class PortfolioItemCodes
        {
            public const string PortfolioItemAddButtonText = "IC-PI0001";
            public const string AddPortfolioItemModalTitleText = "AddPortfolioItemModalTitleText";
            public const string AddPortfolioItemModalButtonText = "AddPortfolioItemModalButtonText";
            public const string PortfolioItemNameModalFormText = "PortfolioItemNameModalFormText";
            public const string PortfolioItemNameModalFormPlaceholderText = "PortfolioItemNameModalFormPlaceholderText";
            public const string PortfolioItemNameTableHeaderText = "IC-PI0002";
            public const string PortfolioItemTickerTableHeaderText = "IC-PI0003";
            public const string PortfolioItemLogoTableHeaderText = "IC-PI0004";
            public const string PortfolioItemActionTableHeaderText = "IC-PI0005";
            public const string PortfolioItemTickerModalFormText = "IC-PI0006";
            public const string PortfolioItemTickerModalFormPlaceholderText = "IC-PI0007";
            public const string DeletePortfolioItemConfirmationBody = "IC-PI0008";
            public const string PortfolioItemDeletionConfirmationMessage = "IC-PI0009";
            public const string PortfolioItemDeletionCancellationMessage = "IC-PI0010";
        }

        public static class CrudCodes
        {
            public const string AddButtonText = "AddButtonText";
        }

        public static class ModalCodes
        {
            public const string ConfirmationQuestion = "ConfirmationQuestion";
            public const string DeleteModalConfirmationYes = "IC-MC0001";
            public const string DeleteModalConfirmationNo = "IC-MC0002";
        }
    }

    public static class AssetCodes
    {
        public const string AssetInsertionSuccess = "SC-AS0001";
        public const string AssetInsertionError = "EC-AS0001";
    }

    public static class TitleMessageCodes
    {
        public const string SuccessTitle = "TM-SW0001";
        public const string ErrorTitle = "TM-SW0002";
        public const string InfoTitle = "TM-SW0003";
        public const string WarningTitle = "TM-SW0004";
        public const string CancelledTitle = "TM-SW0005";
        public const string DeletedTitle = "TM-SW0006";
    }

    public static class UserCodes
    {
        public const string UserInsertionSuccess = "SC-US0001";
        public const string UserInsertionError = "EC-US0001";

        public const string UserUpdateSuccess = "SC-US0002";
        public const string UserUpdateError = "EC-US0002";

        public const string UserDeletionSuccess = "SC-US0003";
        public const string UserDeletionError = "EC-US0003";

        public const string UserDuplicateUsernameError = "EC-US0004";

        public const string UserDuplicateEmailError = "EC-US0005";

        public const string UserAcceptTermsAndConditionsError = "EC-US0006";

        public const string UserUsernameCreationError = "EC-US0007";
        public const string UserEmailCreationError = "EC-US0008";
        public const string UserUsernameEmptyError = "EC-US0009";
        public const string UserEmailEmptyError = "EC-US0010";
        public const string UserPasswordEmptyError = "EC-US0011";
        public const string UserUsernameDoesNotExistError = "EC-US0012";
        public const string UserIncorrectPasswordError = "EC-US0013";
        public const string UserPasswordDoesNotExistError = "EC-US0014";
    }

    public static class PortfolioCodes
    {
        public const string PortfolioInsertionSuccess = "SC-PO0001";
        public const string PortfolioInsertionError = "EC-PO0001";

        public const string PortfolioUpdateSuccess = "SC-PO0002";
        public const string PortfolioUpdateError = "EC-PO0002";

        public const string PortfolioDeletionSuccess = "SC-PO0003";
        public const string PortfolioDeletionError = "EC-PO0003";
    }

    public static class PortfolioItemCodes
    {
        public const string InsertionSuccess = "SC-PI0001";
        public const string InsertionError = "EC-PI0001";

        public const string DeletionSuccess = "SC-PI0002";
        public const string DeletionError = "EC-PI0002";

        public const string PortfolioItemsDeletionSuccess = "SC-PI0003";
        public const string PortfolioItemsDeletionError = "EC-PI0003";
    }

    public static class TransactionCodes
    {
        public const string InsertionSuccess = "SC-TX0001";
        public const string InsertionError = "EC-TX0001";

        public const string UpdateSuccess = "SC-TX0002";
        public const string UpdateError = "EC-TX0002";

        public const string DeletionSuccess = "SC-TX0003";
        public const string DeletionError = "EC-TX0003";

        public const string PortfolioItemsTransactionsDeletionSuccess = "SC-TX0004";
        public const string PortfolioItemsTransactionsDeletionError = "EC-TX0004";

        public const string PortfolioTransactionsDeletionSuccess = "SC-TX0005";
        public const string PortfolioTransactionsDeletionError = "EC-TX0005";
    }

    public static class GenericDatabaseCodes
    {
        public const string GenericDatabaseError = "EC-DB0001";
    }
}