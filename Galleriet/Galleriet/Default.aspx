<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div>
            <asp:Repeater ID="ImageRepeater" runat="server"
                ItemType="Galleriet.Model.Gallery"
                SelectMethod="ImageRepeater_GetData">

            </asp:Repeater>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>

        <%-- Felsummeringslista --%>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Fel har inträffat. Åtgärda fel och försök igen."/>
        </div>

        <%-- Knappar för val av fil & uppladdning --%>
        <div>
            <asp:FileUpload ID="ChooseFileUpload" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ErrorMessage="Välj en bild att ladda upp"
                ControlToValidate="ChooseFileUpload"
                Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="Filen måste vara av typen .jpg/.gif/.png"
                ControlToValidate="ChooseFileUpload"
                ValidationExpression="^.*\.(gif|jpg|png)$"
                Display="None"></asp:RegularExpressionValidator>

            <asp:Button ID="UploadButton" runat="server" Text="Button" OnClick="UploadButton_Click" />
        </div>

    </div>
    </form>
</body>
</html>
