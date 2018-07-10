Imports MyBooks.Models

Namespace Global.MyBooks.Repository

    Public Interface IAuthorRepository

        ' Returns all storages. 
        Function GetAsync() As Task(Of IEnumerable(Of Author))

        ' Returns all storages with a data field matching the start of the given string. 
        Function GetAsync(search As String) As Task(Of IEnumerable(Of Author))

        ' Returns the storage with the given id. 
        Function GetAsync(id As Guid) As Task(Of Author)

        ' Adds a new storage if it does not exist
        Function Insert(author As Author) As Task

    End Interface

End Namespace
