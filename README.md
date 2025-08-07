#  Character Customization System

Sistem ini merupakan bagian dari proyek Unity yang dirancang untuk mendukung **customisasi karakter 2D** secara modular. Cocok digunakan dalam game edukasi, RPG, atau game anak-anak yang memerlukan sistem visual interaktif untuk memilih tampilan karakter.

##  Fitur Utama

### 1.  Skin Library (Next & Previous)
Pengguna dapat memilih berbagai variasi tampilan karakter, seperti rambut, pakaian, atau aksesoris menggunakan tombol `Next` dan `Previous`.

- Sistem dibangun menggunakan `Sprite Resolver`.
- Label sprite digunakan untuk mengganti bagian tubuh tertentu.
- Dapat diatur untuk setiap bagian (contoh: kepala, rambut, badan).

### 2.  Skin Color Switcher
Memungkinkan perubahan warna kulit karakter secara real-time melalui sistem pemilihan warna.

- Warna diatur menggunakan sistem kode warna (`Color`) di Inspector.
- Bagian tubuh yang tergolong *skin* dikelompokkan berdasarkan label dan diganti warnanya secara seragam.

### 3.  Salsa LipSync Integration
Sistem ini terintegrasi dengan plugin **Salsa LipSync** untuk memberikan animasi gerakan mulut berdasarkan suara.

- Menggunakan komponen `Salsa2D` untuk membaca input audio/mic atau clip.
- Otomatis menggerakkan mulut karakter sesuai dialog.

### 4. 🎬 Animation Trigger Button
Tombol UI yang memungkinkan pemutaran animasi sekali saat diklik.

- Menggunakan `Animator.SetTrigger()`.
- Contoh implementasi: karakter melambaikan tangan saat tombol ditekan.


