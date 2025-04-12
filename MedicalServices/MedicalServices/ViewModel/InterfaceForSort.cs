using MedicalServices.Models;

namespace MedicalServices.ViewModel
{
    public interface IBaseInterface
    {
    
    }
    public interface InterfaceForSort : IBaseInterface
    {
        string Name { get; set; }
    }
    public interface InterfaceForSortBranchId : IBaseInterface
    {
        int? BranchID { get; set; }
    }
    public interface InterfaceForBGS : IBaseInterface
    {
        Guset Guset { get; set; }
    }
    public interface InterfaceByID: IBaseInterface
    {
        int ID { get; set; }
    }
    public interface InterfaceByDate : IBaseInterface
    {
        DateBranch DateBranch { get; set; }
    }




}
