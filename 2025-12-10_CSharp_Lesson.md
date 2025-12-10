# C# Mastery: 3-Month Plan
## Date: 2025-12-10
### Month 1, Week 2, Day 8: Continuing from Day 5 (if-else Statements) - Level 9 Completion

**Session Status:** Completed

**Today's Goals:**
- ✓ Complete Level 9 (Expert): Restaurant Order System
- ✓ Demonstrate mastery of complex conditional logic
- ✓ Document all Day 5 solutions in CSharp_Task_Solutions.md
- ✓ Prepare for next topic: Loops and Iteration

---

## **Session Summary**

Today's session focused on completing the final Test_Agent challenge (Level 9) from Day 5's if-else statements topic, followed by comprehensive documentation updates.

---

## **LEVEL 9: EXPERT (Very Challenging) - COMPLETED**

**Challenge:** Restaurant Order System with Complex Business Rules

**Final Solution (PASSED):**

```csharp
Console.WriteLine($"==== Restaurant Order System with Complex Business Rules ====");
// Ask for user input
Console.Write($"Enter the meal type (breakfast, lunch or dinner): ");
string mealType = Console.ReadLine().ToLower();
Console.Write($"Enter the number of people: ");
int numberOfPeople = int.Parse(Console.ReadLine());
Console.Write($"Day of the week (Weekday or Weekend): ");
string dayOfWeek = Console.ReadLine().ToLower();
Console.Write($"Has coupon (yes or no): ");
string hasCoupon = Console.ReadLine().ToLower();
Console.Write($"Is Member (yes or no): ");
string isMember = Console.ReadLine().ToLower();

// Pricing rules for base meal per person including weekend adjustments
decimal breakfastPrice = 12.00m;
decimal lunchPrice = 18.00m;
decimal dinnerPrice = 25.00m;
decimal baseMealPrice = 0.00m;
decimal basePricePerPerson = 0.00m;

if (mealType == "breakfast")
{
    basePricePerPerson = breakfastPrice;
    baseMealPrice = numberOfPeople * breakfastPrice;
    if (dayOfWeek == "weekend")
    {
        basePricePerPerson += 3.00m;
        baseMealPrice += 3.00m * numberOfPeople;
    }
}
else if (mealType == "lunch")
{
    basePricePerPerson = lunchPrice;
    baseMealPrice = numberOfPeople * lunchPrice;
    if (dayOfWeek == "weekend")
    {
        baseMealPrice += 0.00m * numberOfPeople;
    }
}
else if (mealType == "dinner")
{
    basePricePerPerson = dinnerPrice;
    baseMealPrice = numberOfPeople * dinnerPrice;
    if (dayOfWeek == "weekend")
    {
        basePricePerPerson += 5.00m;
        baseMealPrice += 5.00m * numberOfPeople;
    }
}
else
{
    Console.WriteLine($"Choose a correct meal type or day of week");
}

// Calculate initial total per person
decimal subTotalPrice = baseMealPrice;
decimal groupDiscountAmount = 0.00m;
int groupDiscountPercentage = 0;

// Group discount
if (numberOfPeople <= 2)
{
    groupDiscountPercentage = 0;
    groupDiscountAmount = 0.00m;
}
else if (numberOfPeople <= 5)
{
    groupDiscountPercentage = (int)(0.05 * 100);
    groupDiscountAmount = subTotalPrice * 0.05m;
}
else
{
    groupDiscountPercentage = (int)(0.10 * 100);
    groupDiscountAmount = subTotalPrice * 0.10m;
}

subTotalPrice -= groupDiscountAmount;

// Discount eligibility rules for coupon and membership
int couponDiscountPercentage = 0;
decimal couponDiscountAmount = 0.00m;
int membershipDiscountPercentage = 0;
decimal membershipDiscountAmount = 0.00m;

if (hasCoupon == "yes" && subTotalPrice > 50.00m)
{
    couponDiscountPercentage = (int)(0.15 * 100);
    couponDiscountAmount = subTotalPrice * 0.15m; // Additional 15% coupon discount
    subTotalPrice -= couponDiscountAmount;
}

if (isMember == "yes")
{
    membershipDiscountPercentage = (int)(0.10 * 100);
    membershipDiscountAmount = subTotalPrice * 0.10m; // Additional 10% member discount
}

decimal finalTotalPrice = baseMealPrice - (membershipDiscountAmount + couponDiscountAmount + groupDiscountAmount);

// Output the final total price
Console.WriteLine($"Base price per person: ${basePricePerPerson:F2}\nNumber of people: {numberOfPeople}\nSubtotal: ${baseMealPrice:F2}\nGroup discount: ${groupDiscountAmount:F2} ({groupDiscountPercentage}%)\nCoupon discount: ${couponDiscountAmount:F2} ({couponDiscountPercentage}%)\nMember discount: ${membershipDiscountAmount:F2} ({membershipDiscountPercentage}%)\nFinal total: ${finalTotalPrice:F2}");
```

---

## **Test_Agent Final Evaluation**

**LEVEL 9: PASS** ✓✓✓

**🎉 CONGRATULATIONS! All 9 levels completed - MASTERY ACHIEVED!**

### **Verification Results:**

**Test Case 1: Simple case - no discounts** ✓
- All values match expected output perfectly

**Test Case 2: Weekend dinner with group discount** ✓
- Weekend upcharge correctly added to per-person price ($25 + $5 = $30)
- Group discount applied correctly

**Test Case 3: All discounts apply** ✓
- Complex calculation flow executed perfectly
- Subtotal: $108.00 → After group (10%): $97.20 → After coupon (15%): $82.62 → After member (10%): $74.36

**Test Case 4: Coupon threshold not met** ✓
- Coupon correctly not applied (34.20 < 50)

### **Key Improvements Made:**

1. **Fixed weekend upcharge calculation** - Now updates both `basePricePerPerson` and total
2. **Fixed member discount percentage display** - Changed from 15% to 10%
3. **Separated member discount logic** - No longer nested in coupon check
4. **Fixed coupon threshold operator** - Changed from `>=` to `>`

---

## **What Made This Solution Excellent**

### **1. Correct Calculation Order**
The solution follows the complex calculation flow perfectly:
- Base price + weekend upcharge → Subtotal
- Subtotal - group discount → After group discount
- After group discount - coupon (if eligible) → After coupon
- After coupon - member discount (if eligible) → Final total

### **2. Proper Conditional Independence**
Coupon and member checks are correctly separated:
```csharp
if (hasCoupon == "yes" && subTotalPrice > 50.00m)
{
    // Apply coupon
}
if (isMember == "yes")  // SEPARATE check, not nested
{
    // Apply member discount
}
```

### **3. Sequential Discount Application**
Each discount correctly updates `subTotalPrice` so subsequent discounts apply to the reduced amount.

### **4. Accurate Decimal Arithmetic**
All calculations use `decimal` type ensuring precision for monetary values.

---

## **Day 5 Complete Assessment: if-else Statements**

### **All 9 Levels Completed:**

- ✓ **Level 1:** Age Classifier (Foundation)
- ✓ **Level 2:** Number Sign Checker (Foundation)
- ✓ **Level 3:** Grade Calculator (Application)
- ✓ **Level 4:** Login Validator (Application)
- ✓ **Level 5:** Ticket Pricing System (Integration)
- ✓ **Level 6:** Shipping Cost Calculator (Integration)
- ✓ **Level 7:** ATM Withdrawal System (Mastery)
- ✓ **Level 8:** Parking Fee Calculator (Mastery)
- ✓ **Level 9:** Restaurant Order System (Expert) ← **COMPLETED TODAY**

### **Skills Demonstrated:**

✓ Understanding boolean expressions and comparison operators
✓ Using AND (`&&`) and OR (`||`) logical operators correctly
✓ Choosing the right conditional pattern (if, if-else, else-if, nested)
✓ Writing sequential validation logic (guard clauses)
✓ Applying business rules in the correct order
✓ Handling edge cases and thresholds
✓ Using decimal arithmetic for financial calculations
✓ Formatting output for user-friendly display
✓ Separating independent conditional checks
✓ Debugging and fixing logic errors

---

## **Documentation Updates Completed**

### **CSharp_Task_Solutions.md**

Added comprehensive documentation for all Day 5 exercises including:

1. **All 9 level solutions** with explanations
2. **First attempt failures** and corrections (Levels 3, 5, 7, 9)
3. **Model solutions** with optimizations where applicable
4. **Test_Agent evaluation feedback** for each level
5. **Summary statistics** for Day 5 performance
6. **Overall achievement summary** (45 total challenges completed)

**Key Statistics Added:**
- Day 5 Pass Rate: 100%
- Total Retries: 4
- Overall Progress: 5 major concepts mastered
- Ready to advance to: Loops and Iteration

---

## **Session Achievements**

**Today's Accomplishments:**
1. ✓ Completed Level 9 (Expert) - Most challenging if-else assessment
2. ✓ Achieved 100% pass rate on all Day 5 challenges
3. ✓ Documented all solutions with detailed explanations
4. ✓ Added model solutions for optimization learning
5. ✓ Updated comprehensive statistics and progress tracking

**Learning Outcomes:**
- Mastered complex conditional logic with multiple dependencies
- Demonstrated ability to debug and correct logic errors
- Showed understanding of calculation order in sequential workflows
- Applied professional practices (decimal for money, proper formatting)
- Exhibited persistence and problem-solving skills

---

## **Next Steps: Ready for Loops and Iteration**

With if-else statements fully mastered, you're now prepared to advance to **Week 2, Day 9: Loops and Iteration**, which will cover:

1. **While loops** - Condition-based repetition
2. **Do-while loops** - Execute-first loops
3. **For loops** - Counter-based iteration
4. **Foreach loops** - Collection iteration
5. **Loop control** - Break and continue statements
6. **Nested loops** - Loops within loops

**Foundation Established:**
- ✓ Console I/O
- ✓ Variables and data types
- ✓ Operators and expressions
- ✓ User input and type conversion
- ✓ **Conditional logic (if-else statements)**

**Up Next:** Adding repetition and iteration to your programming toolkit! 🚀

---

**Session End Time:** 2025-12-10
**Status:** Day 5 Complete - Ready to advance to Day 6 (Loops)
**Overall Progress:** 5/12 Week 1-2 Topics Completed (42%)

---

*Generated by csharp_prof teaching agent*
*All solutions verified by Test_Agent assessment system*
