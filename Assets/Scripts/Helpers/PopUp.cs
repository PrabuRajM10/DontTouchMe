namespace Helpers
{
    public static class PopUp
    {
        private static PopUpUI _popUpUI;
        
        public static void Init(PopUpUI popUp)
        {
            _popUpUI = popUp;
        }

        public static void ShowPopUp(string header , string message , PopUpType popUpType , PopUpUI.YesCallBack yesCallBack, PopUpUI.NoCallBack noCallBack)
        {
            _popUpUI.ShowPop(header,message , popUpType , yesCallBack , noCallBack);
        }

        public static void ClosePopUp()
        {
            _popUpUI.ClosePopUp();
        }
    }

    public enum PopUpType
    {
        YES_NO,
        ONLY_YES,
        ONLY_NO,
        CONFIRM,
        CONTINUE
    }
}