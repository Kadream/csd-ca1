Feature: Blood pressure category
  In order to understand my blood pressure as a patient
  I want my reading to be placed in the correct category

  Scenario Outline: Calculate the blood pressure category
    Given a systolic pressure of <systolic> mmHg
    And a diastolic pressure of <diastolic> mmHg
    When the blood pressure category is calculated
    Then the category should be "<category>"

    Examples:
      | systolic | diastolic | category |
      |       85 |        55 | Low      |
      |      110 |        70 | Ideal    |
      |      130 |        75 | PreHigh  |
      |      150 |        95 | High     |

  Scenario: Reject a reading where systolic is not higher than diastolic
    Given a systolic pressure of 80 mmHg
    And a diastolic pressure of 85 mmHg
    When the reading is submitted
    Then the reading should be rejected