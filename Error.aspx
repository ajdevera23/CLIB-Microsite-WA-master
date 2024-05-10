<%@ Page MasterPageFile="~/Dashboard.Master" Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ContentPlaceHolderID="bodyContentForm" runat="server">
    <form id="form1" runat="server">
        <div id="error" style="width: 98.8%; border: #aaaaaa solid 1px; color: #101010; padding: 5px 5px 27px 4px;">
        <h2 style="display: block; height: 22px; background: url(img/left_head_1px.gif) 0 0 repeat-x;
            padding: 4px 0 0 14px; margin: 0 0 1px 0;">
            <span style="padding: 0 0 0 16px; font-size: 12px; font-weight: bold; color: #101010;
                background-position: 0px 3px; background-attachment: scroll; background-image: url(img/arrow.gif);
                background-repeat: no-repeat;">Error Page </span>
        </h2>
        <div style="margin-top: 20px; margin-left: 10px; margin-right: 10px">
            <span style="color: Red; font-family: Arial; font-size: 26px; font-weight: bold;">The
                page you're trying to view has encountered an unexpected problem. </span>
            <hr />
            <table style="margin-top: 20px; width: 100%">
                <tr style="height: 30px">
                    <td colspan="2" style="font-family: Arial; font-size: 19px; font-weight: bold; color: Maroon;">
                        What Happened:
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 53px">
                        &nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblWhat" runat="server" Text="There was an unexpected error in this page. This may be due to a programming bug."
                            Font-Names="Arial" Font-Size="12px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td colspan="2" style="font-family: Arial; font-size: 19px; font-weight: bold; color: Maroon;">
                        How this will affect you:
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 53px">
                        &nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblHow" runat="server" Text="The current page will not load." Font-Names="Arial"
                            Font-Size="12px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td colspan="2" style="font-family: Arial; font-size: 19px; font-weight: bold; color: Maroon;">
                        What you can do about it:
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 53px">
                        &nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblDo" runat="server" Text="Try navigating back to this page and try repeating your last action. Try alternative methods of performing the same action. If problem persist, contact cebuanacares@pjlhuillier.com."
                            Font-Names="Arial" Font-Size="12px" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
   </asp:Content>
