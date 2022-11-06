# RSA algorithm C#

# Description
 
The acronym RSA comes from the names of Ron Rivest, Adi Shamir and Leonard Adleman. These are the authors who described the algorithm in 1977. The RSA algorithm is an asymmetric cryptographic algorithm. Asymmetric means that it works on two different keys, i.e. a public key and a private key, which are mathematically related to each other:
- *public key* - the key used to encrypt a given message,
- *private key* - the key used to decrypt a message, which is inaccessible to others.

The user of the RSA algorithm creates a public key based on two large prime numbers and some auxiliary number. This key is publicly available and anyone can use it to encrypt a message. However, decryption is only possible with the use of the private key by someone who is in possession of it.

The RSA algorithm is very difficult to break. Its security is based on the difficulty of factorizing the product of two large prime numbers. Multiplying the two numbers is easy, but determining the original prime numbers from the sum (factorization) is considered unfeasible. This problem is known as the RSA problem. The algorithm is relatively slow in operation and as a result, it is not as widely used.
 
# How it works?

## Generate the keys:

1. Select two large prime numbers *x* and *y*.
2. Calculate $n = x * y$.
3. Calculate the *totient* function $\phi(n) = (x-1)(y-1)$.
4. Select an integer *e* that is co-prime to $\phi(n)$ and 1 < *e* < $\phi(n)$. Two integers are co-prime if the only positive integer that divides them is 1.
5. Create the public key as a pair of numbers (*n*, *e*).
6. Calculate *d* such that $(d * e) &ensp; mod &ensp; \phi(n) = 1$. It can be found using the extended euclidean algorithm.
7. Create the private key as a pair of numbers (*n*, *d*).

### Encryption process

The plaintext message M is encrypted with a public key. To get the ciphertext from the plaintext, use the following formula to get the ciphertext C.

$$C = P^e &ensp; mod &ensp; n$$

### Decryption process

To decrypt the message the private key is used. The plaintext P can be found using following formula.

$$P = C^d &ensp; mod &ensp; n$$

# Launch

To run the program, you can open the project using the Visual Studio IDE and simply click Run, or you can navigate to the RSA folder, where there is an RSA.exe file that you can run directly.

Before running the program, the public and private key directory and the **primeNumbers.txt** file with prime numbers (needed to generate new keys) must be provided.

After starting the program, the user will see the start menu. At first, the user is asked to generate new keys. If you press *yes*, new keys will be generated and stored in a specific location that you have to determine. If you choose *no*, existing keys will be used. Note that the keys must exist before using the application. In the next steps you will be asked to enter the message to be encrypted with the RSA algorithm. After that, the decryption process will start automatically. The menu is a basic structure used only to check if the program works correctly. You have the possibility to edit it and create your own options.

# Sources

The description is based on the sources listed below.
1. https://www.educative.io/answers/what-is-the-rsa-algorithm
2. https://en.wikipedia.org/wiki/RSA_(cryptosystem)
3. https://www.geeksforgeeks.org/rsa-algorithm-cryptography/
4. https://www.techtarget.com/searchsecurity/definition/RSA
