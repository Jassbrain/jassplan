JassTools 

This project is just a place to put some common functions 
that are complex enough to deserve their own testing and 
reuse across project. This project is the lowest layer in the 
JassPlan Solution and implements the following interfaces.
(Alwways check the code for latest version of course)


    interface IJassCommonAttributesMapper<T1, T2, T3>
        /* objects of this class allows to efficiently deal with objects of two classes that share common attributes 
        for example, if o2:T2 and o3:T3 are objects of completely different classes but both have a "Name" attribute, when we apply 
         map(o2,o3) we will copy the attribute o2.Name into o3.Name. The purpose of the type T1 is to contrain those common attributes to
         a predefined set. So, basically, the map between o2 and o3 will accur only for those common attributes between T1,T2,T3
         The most nornmal use is for cases when T2:T1 and T3:T1 but we decided not to constrain this explicitelly allowing T2
         and T3 to share attributes with T1 without necessarilly being subclasses of T1*/
    {
        bool compare(T2 o2, T3 o3); //Compares o2 and o3 making shure every attribute in T1 has the same value.
        void map(T2 o2, T3 o3); //maps every attribute in T1 from o2 to o3
    }

