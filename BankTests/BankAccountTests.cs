using BankAccountNS;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

/*You can have other classes in a unit test project that do not have the [TestClass] attribute, 
  and you can have other methods in test classes that do not have the [TestMethod] attribute. 
  You can call these other classes and methods from your test methods.*/

namespace BankTests {
    [TestClass]           //required on any class that contains unit test methods that you want to run in Test Explorer.
    public class BankAccountTests {

        /* 3 behaviors that need to be checked:
            1. The method throws an ArgumentOutOfRangeException if the debit amount is greater than the balance.
            2. The method throws an ArgumentOutOfRangeException if the debit amount is less than zero.
            3. If the debit amount is valid, the method subtracts the debit amount from the account balance.*/

        [TestMethod]     //Each test method that you want Test Explorer to recognize must have the [TestMethod] attribute.
        public void Debit_WithValidAmount_UpdatesBalance() {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            // Assert.AreEqual: verify that the ending balance is as expected
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        /* case when the amount withdrawn is greater than the balance, do the following steps:
            1. Create a new test method named Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange.
            2. Copy the method body from Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange to the new method.
            3. Set the debitAmount to a number greater than the balance.*/
           
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange() {
           // Arrange
           double beginningBalance = 11.99;
           double debitAmount = -100.00;
           BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

           // Act and assert
           Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));

        }
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange() {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e) {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }
    }
}