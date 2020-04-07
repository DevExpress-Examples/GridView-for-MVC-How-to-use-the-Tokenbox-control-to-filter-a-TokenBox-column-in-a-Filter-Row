@Code
    ViewData("Title") = "Home Page"
End Code

@Html.DevExpress().CheckBox(
            Sub(settings)
                settings.Name = "CheckBox1"
                settings.Text = "AND/OR"
                settings.Checked = False
                settings.Init = Sub(s, e)
                                    Dim CheckBox As MVCxCheckBox = TryCast(s, MVCxCheckBox)
                                    CheckBox.ClientSideEvents.CheckedChanged = "function(s,e){GridView.PerformCallback()}"
                                End Sub
            End Sub
).GetHtml()
@Html.Action("GridViewPartial")

