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
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Friend Class MockAccountPositionService
        Implements IAccountPositionService
        Private positionData As New List(Of AccountPosition)()

        Public Sub AddPosition(ByVal ticker As String, ByVal costBasis As Decimal, ByVal shares As Long)
            AddPosition(New AccountPosition(ticker, costBasis, shares))
        End Sub

        Public Sub AddPosition(ByVal position As AccountPosition)
            AddHandler position.Updated, AddressOf position_Updated
            positionData.Add(position)

            'Notify that collection has changed
            RaiseEvent Updated(Me, New AccountPositionModelEventArgs(position))
        End Sub

        Private Sub position_Updated(ByVal sender As Object, ByVal e As AccountPositionEventArgs)
            RaiseEvent Updated(Me, New AccountPositionModelEventArgs(TryCast(sender, AccountPosition)))
        End Sub

#Region "IAccountPositionService Members"

        Public Function GetAccountPositions() As IList(Of AccountPosition) _
            Implements IAccountPositionService.GetAccountPositions
            Return positionData
        End Function

        Public Event Updated As EventHandler(Of AccountPositionModelEventArgs) _
            Implements IAccountPositionService.Updated

#End Region
    End Class
End Namespace
