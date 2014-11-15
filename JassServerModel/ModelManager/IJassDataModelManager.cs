﻿using System;
namespace Jassplan.JassServerModelManager
{
    public interface IJassDataModelManager
    {
        global::System.Collections.Generic.List<global::Jassplan.Model.JassActivity> ActivitiesArchiveGetAll();
        global::System.Collections.Generic.List<global::Jassplan.Model.JassActivityReview> ActivityReviewsGetAll();
        global::System.Collections.Generic.List<global::Jassplan.Model.JassActivity> ActivitiesGetAll();
        int ActivityCreate(global::Jassplan.Model.JassActivity Activity);
        void ActivityDelete(int id);
        void ActivityDeleteAll();
        global::Jassplan.Model.JassActivity ActivityGetById(int id);
        global::Jassplan.Model.JassActivityHistory ActivitySave(global::Jassplan.Model.JassActivity Activity);
        void Dispose();
    }
}