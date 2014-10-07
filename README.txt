JassTools 

This project is just a place to put some common functions 
that are complex enough to deserve their own testing and 
reuse across project. This project is the lowest layer in the 
JassPlan Solution and implements, for now, the following interface

    interface IJassCommonAttributesMapper<T1, T2, T3>
     {
        bool compare(T2 o2, T3 o3); //Compares o2 and o3 making shure every attribute in T1 has the same value.
        void map(T2 o2, T3 o3); //maps every attribute in T1 from o2 to o3
    }

