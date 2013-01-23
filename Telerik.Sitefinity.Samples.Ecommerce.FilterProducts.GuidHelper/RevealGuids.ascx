<%@ Control Language="C#" %>

<h2>Departments</h2>
<table>
    <asp:Repeater ID="rptDepartments" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblId" runat="server"></asp:Label>
                </td>
            </tr>

        </ItemTemplate>
    </asp:Repeater>
</table>


<h2>Product Types</h2>
<table>
    <asp:Repeater ID="rptProductTypes" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblId" runat="server"></asp:Label>
                </td>
            </tr>

        </ItemTemplate>
    </asp:Repeater>
</table>

<h2>Tags</h2>
<table>
    <asp:Repeater ID="rptTags" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblId" runat="server"></asp:Label>
                </td>
            </tr>

        </ItemTemplate>
    </asp:Repeater>
</table>
