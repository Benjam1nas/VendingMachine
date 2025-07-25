namespace Core.Features.Machine.Requests;

public class CreateMachineRequest
{
    public required string Title { get; set; }
    public required string Location { get; set; }
}
