﻿namespace MedicalServices.Repository
{
    public interface IImageRepository
    {
        Task<bool> SaveImageInWroot(IHasImage hasImage, IFormFile ImageFile, string Location = "Images/Imagesadmin/");
        void DeleteImageInWroot(string Image);

        Task<bool> UpdateImageInWroot(string OldImagePath, IHasImage hasImage, IFormFile NewImage);

    }
    public interface IHasImage
    {
        public string Image { get; set; }
    }

}
