# DeciTap+: NFC/Card UID Reader Utility

DeciTap+ is a specialized, lightweight utility designed to bridge the NFC card reader hardware with the main C# WinForms application. Its primary function is to read the unique decimal UID (CardNumber) from an authorized user's ID card and output it directly to a focused application via **Keyboard Wedge (simulated typing)**. It provides real-time, highly configurable output necessary for instant user authentication.

<img width="472" height="285" alt="Screenshot 2025-03-10 141635" src="https://github.com/user-attachments/assets/86632662-8cd1-409d-afa5-00f09971d94f" />


## 🚀 Key Features

* **Always On Top Window:** **The application window remains above all other desktop windows (unless minimized)**, ensuring that the simulated keyboard input (CardNumber) is never inadvertently sent to the wrong program, providing reliability during the authentication setup.

* **NFC Reader Management:** Allows selection from multiple connected NFC/Mifare readers via a dropdown menu (e.g., "ACS ACR1252IMP 1S CL Reader PICC 0").

* **Real-Time UID Output:** Displays the raw, translated 10-digit `CardNumber` upon a successful card tap, offering immediate visual confirmation.

* **Keyboard Wedge Integration (Primary Output):** When enabled, it simulates keyboard presses to automatically type the CardNumber into any active field in the main WinForms application, eliminating the need for manual entry or file reading.

* **Decimal Formatting:** Confirms the output format is set to **Decimal** (the required format for the `Users` table).

* **Configurable Output Length:** Ensures consistency by enforcing a **10-digit Character Length** for the output CardNumber.

* **Output Control:** Includes an option to append an **Enter After Output**, streamlining the authentication workflow by automatically submitting the typed CardNumber.

* **Reader Diagnostics:** Provides a **Refresh** button to re-scan for available NFC readers and a red status bar for real-time feedback (e.g., "Card removed. Place card on the reader to scan.").

## ⚙️ How It Works

The DeciTap+ executable works as a real-time input device:

1. **Listen:** It maintains a connection to the selected NFC reader.

2. **Read & Translate:** Upon a card tap, it reads the raw card UID, translates it to the 10-digit decimal `CardNumber`, and displays it in the output box.

3. **Simulate Input:** If **Keyboard Wedge** is enabled, DeciTap+ simulates a user typing the 10-digit `CardNumber` into the active input field of the main WinForms application.

4. **Confirm:** If **Enter After Output** is enabled, it simulates the ENTER key press, automatically triggering the login or transaction lookup function in the WinForms application.

## 🛠️ Prerequisites

To run and utilize DeciTap+, you will need:

* **Operating System:** Windows (compatible with .NET Framework).

* **NFC Reader:** A compatible NFC/Mifare card reader (e.g., ACS ACR1252IMP) connected via USB.

* **C# WinForms Application:** The main scrub management application built in C# that is ready to accept the CardNumber input via simulated keyboard presses.


## 📋 Installation and Setup

1. **Clone the Repository:**

2. **Build and Deploy:**
Open the solution file in Visual Studio and build the project in **Release** mode. The resulting DeciTap executable (`DeciTap.exe`) should be deployed to the machine where the NFC reader is located.

3. **Configuration (Critical):**

* In DeciTap+, ensure the correct reader is selected.

* Set **Format** to `Decimal` and **Character Length** to `10`.

* Check the **Keyboard Wedge** box and, optionally, **Enter After Output**.

## 🚀 Usage

1. **Run the DeciTap.exe executable.** Due to its "Always On Top" feature, you can either leave it visible for monitoring or minimize it for a cleaner desktop view, ensuring it remains active in the background.

2. In the main C# WinForms application, ensure the authentication input field (e.g., a Card Number textbox) is focused.

3. **Authentication:** When a user taps their ID card on the NFC reader, DeciTap+ instantly inputs the `CardNumber` into the focused textbox.

4. The WinForms application immediately receives the `CardNumber` and executes a SQL `SELECT` query against the `Users` table: `SELECT * FROM Users WHERE CardNumber = '[Input_CardNumber]'` to authenticate the user and start the transaction.

## 🔗 Connection to Scrub Management System

DeciTap+ simplifies the user authentication flow by handling the physical card reading and formatting the output correctly. It ensures the integrity of the process by providing the unique identifier required by the WinForms application to look up a user's details, check their `CreditBalance`, and record a new transaction in the `Transactions` table.
