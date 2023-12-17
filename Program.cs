using System.Text.Json;
using models;

class Program{  
    
    static async Task Main(string[] args){
        var tasks = await ReadFile();
        if (tasks.Count == 0) Console.WriteLine("No tienes tareas");

        Console.WriteLine("*********************************************");
        Console.WriteLine("Estas son tus tareas: ");
        Console.WriteLine("  ");
        tasks.ForEach(items => {
            PrintTask(items);
            Console.WriteLine("-------------------------------");
        });        

        Console.WriteLine("  ");
        Console.WriteLine("Ingresa el numero de tarea que deseas realizar");
        
        int taskNum;
        int.TryParse(Console.ReadLine(),out taskNum);
        if (taskNum > tasks.Count || taskNum == 0){
            Console.WriteLine("Numero invalido");
            return;
        }

        var newTasks = tasks.Select(item => {
            if (item.Id == taskNum){
                item.Finished = true;  
                Console.WriteLine("-------------");  
                PrintTask(item);
                Console.WriteLine("-------------");  
            }

            return item;
        }).ToList();
        await WriteFile(newTasks);

        Console.WriteLine("Programa terminado *********************************************");
    }

    static async Task<List<TaskModel>> ReadFile(){
        string filePath = "tasks.json";

        if(!File.Exists(filePath)) {
            Console.WriteLine($"El archivo no existe en la ruta {filePath}");
            return new List<TaskModel>();
        } 

        string content = await File.ReadAllTextAsync(filePath);
        List<TaskModel>? tasks =  JsonSerializer.Deserialize<List<TaskModel>>(content);
        return tasks!;
    }

    static async Task WriteFile(List<TaskModel> tasks){
        string filePath = "tasks.json";

        string jsonContent = JsonSerializer.Serialize(tasks);
        await File.WriteAllTextAsync(filePath,jsonContent);

        Console.WriteLine("Tarea guardada");
    }

    static void PrintTask(TaskModel task) {
        string finalizado = task.Finished ? "Si ✅" : "No ❌";

        Console.WriteLine($"Numero: {task.Id}");
        Console.WriteLine($"Nombre: {task.Name}");
        Console.WriteLine($"Descripcion: {task.Description}");
        Console.WriteLine($"Finalizado: {finalizado}");
    }
}