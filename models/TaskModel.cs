namespace models;
class TaskModel{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool Finished { get; set; }
    public string? CreatedAt { get; set; }

}