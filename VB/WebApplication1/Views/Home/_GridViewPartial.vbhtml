@ModelType List(Of WebApplication1.WebApplication1.Models.Customer)
@Imports DevExpress.Data.Filtering
@Imports WebApplication1.WebApplication1.Code
@Functions
    Public Sub OnProcessColumnAutoFilter(ByVal sender As Object, ByVal e As ASPxGridViewAutoFilterEventArgs)
        If e.Column.FieldName <> "Licenses" Then Return

        If e.Kind = GridViewAutoFilterEventKind.CreateCriteria Then
            Dim ids = e.Value.Split(";"c)
            Dim operandProp = New OperandProperty(e.Column.FieldName)
            Dim criteriaList = Enumerable.Range(0, ids.Length).[Select](Function(s) New FunctionOperator(FunctionOperatorType.Contains, operandProp, ids(s)))
            Dim groupOperatorType As GroupOperatorType = If(ViewBag.andOrValue, GroupOperatorType.[And], GroupOperatorType.[Or])
            Dim criteria = New GroupOperator(groupOperatorType, criteriaList)
            e.Criteria = criteria
        Else
            e.Value = CriteriaHelper.ExtractDisplayText(e.Criteria)
        End If
    End Sub

    Public Sub OnAutoFilterCellEditorCreate(ByVal sender As Object, ByVal e As ASPxGridViewEditorCreateEventArgs)
        If e.Column.FieldName <> "Licenses" Then Return
        e.EditorProperties = New TokenBoxProperties()
        ConfigureTokenBoxProperties(TryCast(e.EditorProperties, TokenBoxProperties))
    End Sub

    Public Sub ConfigureTokenBoxProperties(ByVal tokenBoxSettings As TokenBoxProperties)
        If tokenBoxSettings Is Nothing Then tokenBoxSettings = New TokenBoxProperties()
        tokenBoxSettings.ValueField = "LicenseID"
        tokenBoxSettings.TextField = "LicenseName"
        tokenBoxSettings.ValueSeparator = ";"c
        tokenBoxSettings.AllowCustomTokens = False
        tokenBoxSettings.DataSource = ViewBag.Licenses
        tokenBoxSettings.ItemValueType = GetType(Int32)
        tokenBoxSettings.Width = 300
    End Sub
End Functions

@Html.DevExpress().GridView(Sub(settings)
                                     settings.Name = "GridView"
                                     settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}

                                     settings.CommandColumn.Visible = True
                                     settings.CommandColumn.ShowEditButton = True
                                     settings.Columns.Add("CustomerID")
                                     settings.Columns.Add("CustomerName")
                                     settings.Columns.Add(Sub(column)
                                                              column.FieldName = "Licenses"
                                                              column.ColumnType = MVCxGridViewColumnType.TokenBox
                                                              column.Settings.AllowSort = DefaultBoolean.True
                                                              Dim TokenBoxSettings = TryCast(column.PropertiesEdit, TokenBoxProperties)
                                                              ConfigureTokenBoxProperties(TokenBoxSettings)
                                                          End Sub)

                                     settings.Settings.AutoFilterCondition = AutoFilterCondition.Contains

                                     settings.KeyFieldName = "CustomerID"
                                     settings.Settings.ShowFilterRow = True

                                     settings.ProcessColumnAutoFilter = Sub(s, e)
                                                                            OnProcessColumnAutoFilter(s, e)
                                                                        End Sub
                                     settings.AutoFilterCellEditorCreate = Sub(s, e)
                                                                               OnAutoFilterCellEditorCreate(s, e)
                                                                           End Sub
                                     settings.ClientSideEvents.BeginCallback = "function(s,e){e.customArgs['andOrValue'] = CheckBox1.GetValue()}"
                                 End Sub
                                 ).Bind(Model).GetHtml()
