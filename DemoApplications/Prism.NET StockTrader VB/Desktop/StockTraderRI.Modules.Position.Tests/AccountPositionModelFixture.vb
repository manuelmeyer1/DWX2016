'===================================================================================
' Microsoft patterns & practices
' Composite Application Guidance for Windows Presentation Foundation and Silverlight
'===================================================================================
' Copyright (c) Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===================================================================================
' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
'===================================================================================
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Position.Services

Namespace StockTraderRI.Modules.Position.Tests
    ''' <summary>
    ''' Summary description for AccountPositionModelFixture
    ''' </summary>
    <TestClass()> _
    Public Class AccountPositionModelFixture
        <TestMethod()> _
        Public Sub ShouldReturnDefaultPositions()
            Dim model As New AccountPositionService()

            Assert.IsTrue(model.GetAccountPositions().Count > 0, "No account positions returned")
        End Sub

        '[TestMethod]
        'public void AddingPositionFiresUpdatedEvent()
        '{
        '    AccountPositionService model = new AccountPositionService();

        '    bool modelUpdated = false;
        '    AccountPosition testAccountPosition = null;

        '    model.Updated += delegate(object sender, AccountPositionModelEventArgs e)
        '    {
        '        modelUpdated = true;
        '        testAccountPosition = e.AcctPosition;
        '    };

        '    AccountPosition newAccountPosition = new AccountPosition("test", 1.00m, 1);
        '    model.AddPosition(newAccountPosition);

        '    Assert.IsTrue(modelUpdated);
        '    Assert.AreSame(testAccountPosition, newAccountPosition);

        '}

        '[TestMethod]
        'public void UpdatingPositionFiresUpdatedEvent()
        '{
        '    AccountPositionService model = new AccountPositionService();
        '    AccountPosition accountPosition = new AccountPosition("test", 1.00m, 1);
        '    model.AddPosition(accountPosition);

        '    bool modelUpdated = false;
        '    AccountPosition updatedAccountPosition = null;

        '    model.Updated += delegate(object sender, AccountPositionModelEventArgs e)
        '    {
        '        modelUpdated = true;
        '        updatedAccountPosition = e.AcctPosition;
        '    };

        '    accountPosition.Shares += 1;
        '    accountPosition.CostBasis += .50m;

        '    Assert.IsTrue(modelUpdated);
        '    Assert.AreSame(updatedAccountPosition, accountPosition);
        '}
    End Class
End Namespace
