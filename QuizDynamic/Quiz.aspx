<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="QuizDynamic.Quiz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="dvQuestion" runat="server">
            <h1>
                <asp:Label ID="lblQuestion" runat="server" />
                <asp:HiddenField runat="server" ID="hfQuestionId" />
            </h1>
            <asp:RadioButtonList ID="rbtnOptions" runat="server">
            </asp:RadioButtonList>
            <br />
            <asp:Button ID="btnNext" Text="Next" runat="server" OnClick="Next" />
        </div>
        <div id="dvResult" runat="server" visible="false">
            <h1>
                <asp:Label ID="lblResult" runat="server" />
            </h1>
        </div>
    </div>
    </form>
</body>
</html>
