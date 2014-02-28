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
            <%-- Stor bild & rätt/felmeddelande. --%>
            <asp:Panel ID="SuccessPanel" runat="server" Visible="false">
                <asp:Label ID="SuccesLabel" runat="server" Text="Uppladdningen av bilden lyckades!"></asp:Label>
                <asp:ImageButton ID="CloseImageButton" runat="server" OnClick="CloseImageButton_Click" ImageUrl="~/Content/dialog_close.ico" CausesValidation="false" />
            </asp:Panel>
            <div>
                <asp:Image ID="LargeImage" runat="server" />
            </div>
            <div>
                <asp:Repeater ID="ImageRepeater" runat="server"
                    OnItemDataBound="ImageRepeater_ItemDataBound"
                    ItemType="System.String"
                    SelectMethod="ImageRepeater_GetData">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server"
                            ImageUrl='<%# "~/Content/Images/Thumbs/" + Item %>'
                            NavigateUrl='<%# "?name=" + Item %>' />
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <%-- Felsummeringslista --%>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Fel har inträffat. Åtgärda fel och försök igen." />
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

                <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" />
            </div>

        </div>
    </form>

    <script src="Scripts/jquery-2.1.0.intellisense.js"></script>
    <script src="Scripts/jquery-2.1.0.js"></script>
    <script src="Scripts/jquery-2.1.0.min.js"></script>
</body>
</html>
