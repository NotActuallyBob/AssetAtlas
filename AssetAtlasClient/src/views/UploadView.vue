<template>
    <div class="upload-csv">
      <h3>Upload CSV File</h3>
      <input type="file" accept=".csv" @change="handleFileChange" />
      <button :disabled="!csvFile" @click="uploadFile">Upload</button>
      <p v-if="uploadStatus">{{ uploadStatus }}</p>
    </div>
  </template>
  
  <script lang="ts" setup>
  import { ref } from "vue";
  import axios from "axios";
  
  // Reactive state
  const csvFile = ref<File | null>(null); // Holds the selected CSV file
  const uploadStatus = ref<string>(""); // Status message for the upload
  
  // Handle file selection
  const handleFileChange = (event: Event) => {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
  
    if (file && file.name.endsWith(".csv")) {
      csvFile.value = file;
      uploadStatus.value = ""; // Reset the status
    } else {
      uploadStatus.value = "Please select a valid CSV file.";
      csvFile.value = null;
    }
  };
  
  // Upload the file to the server
  const uploadFile = async () => {
    if (!csvFile.value) {
      uploadStatus.value = "No file selected.";
      return;
    }
  
    // Prepare form data
    const formData = new FormData();
    formData.append("file", csvFile.value);
  
    try {
      const response = await axios.post("/api/ExpensesUpload", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
  
      uploadStatus.value = "File uploaded successfully!";
      console.log(response.data);
    } catch (error) {
      console.error(error);
      uploadStatus.value = "Error uploading file. Please try again.";
    }
  };
  </script>