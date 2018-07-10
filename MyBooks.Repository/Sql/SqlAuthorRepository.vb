Imports Microsoft.EntityFrameworkCore
Imports MyBooks.Models

Namespace Global.MyBooks.Repository.Sql

    Public Class SqlAuthorRepository
        Implements IAuthorRepository

        Private ReadOnly _db As MyBooksContext

        Public Sub New(db As MyBooksContext)
            _db = db
        End Sub

        Public Async Function GetAsync() As Task(Of IEnumerable(Of Author)) Implements IAuthorRepository.GetAsync
            Return Await _db.Authors.AsNoTracking().ToListAsync()
        End Function

        Public Async Function GetAsync(search As String) As Task(Of IEnumerable(Of Author)) Implements IAuthorRepository.GetAsync

            Dim parameters As String() = search.Split(" ")
            Return Await _db.Authors.Where(
                Function(x As Author) parameters.Any(Function(y As String) x.Name.StartsWith(y))
                ).OrderByDescending(
                Function(x As Author) parameters.Count(Function(y As String) x.Name.StartsWith(y))
                ).AsNoTracking().ToListAsync()

        End Function

        Public Async Function GetAsync(id As Guid) As Task(Of Author) Implements IAuthorRepository.GetAsync
            Return Await _db.Authors.AsNoTracking().FirstOrDefaultAsync(Function(x As Author) x.Id = id)
        End Function

        Public Async Function Insert(author As Author) As Task Implements IAuthorRepository.Insert
            Dim hits = Await GetAsync(author.Name)
            If hits Is Nothing OrElse hits.Count = 0 Then
                Await _db.Authors.AddAsync(author)
                Await _db.SaveChangesAsync()
            End If
        End Function

        Public Async Function SetAuthors(authors As List(Of Author)) As Task
            For Each b In _db.Authors
                _db.Entry(b).State = EntityState.Deleted
            Next
            Await _db.SaveChangesAsync()
            _db.Authors.AddRange(authors)
            Await _db.SaveChangesAsync()
        End Function

        Public Async Function AddAuthors(authors As List(Of Author)) As Task
            Dim saveRequired As Boolean

            For Each i In authors
                If Await GetAsync(i.Id) IsNot Nothing Then
                    _db.Entry(i).State = EntityState.Deleted
                    saveRequired = True
                End If
            Next
            If saveRequired Then
                Await _db.SaveChangesAsync()
            End If
            _db.Authors.AddRange(authors)
            Await _db.SaveChangesAsync()
        End Function

    End Class

End Namespace
