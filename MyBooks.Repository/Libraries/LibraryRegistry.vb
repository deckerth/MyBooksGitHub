Namespace Global.MyBooks.Repository.Libraries

    Public Class LibraryRegistry

        Private Shared _current As LibraryRegistry
        Public Shared ReadOnly Property Current As LibraryRegistry
            Get
                If _current Is Nothing Then
                    _current = New LibraryRegistry
                End If
                Return _current
            End Get
        End Property

        Private _libraries As New List(Of ILibraryAccess)
        Public ReadOnly Property Libraries As List(Of ILibraryAccess)
            Get
                Return _libraries
            End Get
        End Property

        Public Sub New()
            _libraries.Add(New DeutscheNationalbibliothek)
            _libraries.Add(New BibliotheksverbundBayern)
            _libraries.Add(New OnlineComputerLibraryCenter)
            _libraries.Add(New LibraryOfCongress)
        End Sub


    End Class

End Namespace
