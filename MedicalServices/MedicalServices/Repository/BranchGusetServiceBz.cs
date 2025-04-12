using MedicalServices.Models;
namespace MedicalServices.Repository
{
    public class BranchGusetServiceBz: IBranchGusetServiceBz
    {
        IGeneralRepository<DateBranch> datebranchgeneralRepository;
        public BranchGusetServiceBz(IGeneralRepository<DateBranch> _datebranchgeneralRepository)
        {
            datebranchgeneralRepository = _datebranchgeneralRepository;
        }

        public void ReturnDateState(DateBranch datebranch,int datebranchid)
        {
            datebranch = datebranchgeneralRepository.GetById(datebranchid);
            datebranch.State = true;
            datebranchgeneralRepository.Update(datebranchid, datebranch);
        }

    }

}
