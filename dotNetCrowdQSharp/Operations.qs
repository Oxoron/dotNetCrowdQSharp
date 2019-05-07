namespace Quantum.dotNetCrowdQSharp
{
    open Microsoft.Quantum.Primitive;
    open Microsoft.Quantum.Canon;
    

    // Q# Laguage features: operation, output value, variables, qubits usage, qubits gates H, M
    operation RandomQubit () : (Result)
    {        
        mutable res = Zero;
        
        using(qubits = Qubit[1])
        {
            ResetAll(qubits);
                
            H(qubits[0]);
            set res = M(qubits[0]);                            


            ResetAll(qubits); // Always clean after yourself
        }              

        return res;        
    }



    // Additional language features: argument, arrays, ranges (for loops)
    operation RandomGuid (count : Int) : (Result[])
    {                  
        mutable res = new Result[count];

        using(qubits = Qubit[count])
        {
            ResetAll(qubits);
                
            for(i in  0..count-1)
            {
                H(qubits[i]);
                set res[i] = M(qubits[i]);                
            }                             

            ResetAll(qubits); // Always clean after yourself
        }              

        return res;              
    }


    // Language feature: CNOT gate, entaglement
    operation BellState () : (Result, Result)
    {                  
        mutable res0 = Zero;
        mutable res1 = Zero;

        using(qubits = Qubit[2])
        {
            ResetAll(qubits);
                
            H(qubits[0]);
            CNOT(qubits[0], qubits[1]);

            set res0 = M(qubits[0]);
            set res1 = M(qubits[1]);

            ResetAll(qubits); // Always clean after yourself
        }              

        return (res0, res1);              
    }



    // Language feature: controlled operator sample
    operation ThreeOfFourState () : (Result, Result)
    {                  
        mutable res0 = Zero;
        mutable res1 = Zero;

        using(qubits = Qubit[2])
        {
            ResetAll(qubits);
                
            H(qubits[0]);
            (Controlled (H)) 
            (
                [qubits[0]], // Controllers array
                qubits[1] // Arguments
            );
            

            set res0 = M(qubits[0]);
            set res1 = M(qubits[1]);

            ResetAll(qubits); // Always clean after yourself
        }              

        return (res0, res1);              
    }


    // Language feature: gate controlled by Zero
    operation OneBitOfFour () : (Result, Result, Result, Result)
    {           
        // Schema: https://algassert.com/quirk#circuit={%22cols%22:[[%22H%22,1,%22H%22],[%22%E2%80%A2%22,1,%22%E2%80%A2%22,%22X%22],[%22%E2%97%A6%22,%22X%22,%22%E2%97%A6%22],[%22X%22,1,%22X%22,%22%E2%80%A2%22]]}
        mutable res0 = Zero;
        mutable res1 = Zero;
        mutable res2 = Zero;
        mutable res3 = Zero;

        using(qubits = Qubit[4])
        {
            ResetAll(qubits);
                
            // Step 1
            H(qubits[0]);
            H(qubits[2]);

            // Step 2
            (Controlled (X)) 
            (
                [qubits[0], qubits[2]], 
                qubits[3] 
            );            

            // Step 3
            X(qubits[0]);
            X(qubits[2]);
            (Controlled (X)) 
            (
                [qubits[0], qubits[2]], 
                qubits[1] 
            );
            X(qubits[0]);
            X(qubits[2]);


            // Step 4
            CNOT(qubits[3], qubits[0]);
            CNOT(qubits[3], qubits[2]);
            

            set res0 = M(qubits[0]);
            set res1 = M(qubits[1]);
            set res2 = M(qubits[2]);
            set res3 = M(qubits[3]);

            ResetAll(qubits); // Always clean after yourself
        }              

        return (res0, res1, res2, res3);              
    }
}
