### Documentación de la API CRUD de Estudiantes

---

#### 1. Obtener Todos los Estudiantes (GetAllStudents)

- **Endpoint:** `/api/student`
- **Método HTTP:** `GET`
- **Descripción:** Recupera la lista de todos los estudiantes registrados en la base de datos. Ejecuta el procedimiento almacenado `sp_student` con `@opt = 1` para obtener todos los estudiantes.

- **Ejemplo de Petición:**

  ```http
  GET /api/student
  ```

- **Respuesta Exitosa:**
  ```json
  {
      "success": true,
      "message": "Lista de estudiantes obtenida con éxito.",
      "data": [
          {
              "studentId": 1,
              "firstName": "John",
              "middleName": "M.",
              "lastName": "Doe",
              "email": "john.doe@example.com",
              "phone": "123-456-7890",
              "studentCreatedOn": "2023-01-01T12:00:00"
          },
          ...
      ]
  }
  ```

- **Errores Potenciales:**
  - `500 Internal Server Error`: Si ocurre un error en la base de datos o un error interno del servidor.

---

#### 2. Obtener Estudiante por ID (GetStudentById)

- **Endpoint:** `/api/student/{id}`
- **Método HTTP:** `GET`
- **Descripción:** Recupera la información de un estudiante específico según su `ID`. Ejecuta el procedimiento almacenado `sp_student` con `@opt = 2` y `@student_id` para obtener un estudiante por su ID.

- **Parámetros de Ruta:**
  - `id` (int): El ID del estudiante a recuperar.

- **Ejemplo de Petición:**

  ```http
  GET /api/student/1
  ```

- **Respuesta Exitosa:**
  ```json
  {
      "success": true,
      "message": "Información obtenida con éxito",
      "data": {
          "studentId": 1,
          "firstName": "John",
          "middleName": "M.",
          "lastName": "Doe",
          "gender": "Male",
          "addressLine": "123 Main St",
          "city": "Metropolis",
          "zipPostcode": "12345",
          "state": "NY",
          "email": "john.doe@example.com",
          "emailType": "personal",
          "phone": "123-456-7890",
          "phoneType": "mobile",
          "countryCode": "US",
          "areaCode": "123"
      }
  }
  ```

- **Errores Potenciales por Validaciones:**
  - `400 Bad Request`: Si no se proporciona un ID válido.
  - `404 Not Found`: Si no se encuentra el estudiante.
  - `500 Internal Server Error`: Si ocurre un error en la base de datos o un error interno del servidor.

---

#### 3. Crear un Nuevo Estudiante (CreateStudent)

- **Endpoint:** `/api/student`
- **Método HTTP:** `POST`
- **Descripción:** Crea un nuevo estudiante en la base de datos. Ejecuta el procedimiento almacenado `sp_student` con `@opt = 3` para insertar un nuevo registro.

- **Cuerpo de la Petición (JSON):**
  ```json
  {
      "firstName": "John",
      "middleName": "M.",
      "lastName": "Doe",
      "gender": "Male",
      "addressLine": "123 Main St",
      "city": "Metropolis",
      "zipPostcode": "12345",
      "state": "NY",
      "email": "john.doe@example.com",
      "emailType": "personal",
      "phone": "123-456-7890",
      "phoneType": "mobile",
      "countryCode": "US",
      "areaCode": "123"
  }
  ```

- **Ejemplo de Petición:**

  ```http
  POST /api/student
  Content-Type: application/json

  {
      "firstName": "John",
      "middleName": "M.",
      "lastName": "Doe",
      "email": "john.doe@example.com",
      "phone": "123-456-7890",
      ...
  }
  ```

- **Respuesta Exitosa:**
  ```json
  {
      "success": true,
      "message": "Estudiante creado con éxito",
      "data": {
          "studentId": 1,
          "firstName": "John",
          "middleName": "M.",
          "lastName": "Doe",
          ...
      }
  }
  ```

- **Errores Potenciales por Validaciones:**
  - `400 Bad Request`: Si faltan datos requeridos.
  - `500 Internal Server Error`: Si ocurre un error en la base de datos o un error interno del servidor.

---

#### 4. Actualizar Datos de un Estudiante (UpdateStudent)

- **Endpoint:** `/api/student/{id}`
- **Método HTTP:** `PUT`
- **Descripción:** Actualiza los datos de un estudiante existente. Ejecuta el procedimiento almacenado `sp_student` con `@opt = 4` y los parámetros actualizados.

- **Parámetros de Ruta:**
  - `id` (int): El ID del estudiante a actualizar.

- **Cuerpo de la Petición (JSON):**
  ```json
  {
      "firstName": "John",
      "middleName": "M.",
      "lastName": "Doe",
      "gender": "Male",
      "addressLine": "123 Main St",
      "city": "Metropolis",
      "zipPostcode": "12345",
      "state": "NY",
      "email": "john.doe@example.com",
      "emailType": "personal",
      "phone": "123-456-7890",
      "phoneType": "mobile",
      "countryCode": "US",
      "areaCode": "123"
  }
  ```

- **Ejemplo de Petición:**

  ```http
  PUT /api/student/1
  Content-Type: application/json

  {
      "firstName": "John",
      "middleName": "M.",
      "lastName": "Doe",
      "email": "john.doe@example.com",
      ...
  }
  ```

- **Respuesta Exitosa:**
  ```json
  {
      "success": true,
      "message": "Estudiante actualizado con éxito",
      "data": {
          "studentId": 1,
          "firstName": "John",
          "middleName": "M.",
          "lastName": "Doe",
          ...
      }
  }
  ```

- **Errores Potenciales por Validaciones:**
  - `400 Bad Request`: Si no se proporciona un ID válido o si faltan datos requeridos.
  - `500 Internal Server Error`: Si ocurre un error en la base de datos o un error interno del servidor.

---

#### 5. Eliminar un Estudiante (DeleteStudent)

- **Endpoint:** `/api/student/{id}`
- **Método HTTP:** `DELETE`
- **Descripción:** Elimina un estudiante específico de la base de datos. Ejecuta el procedimiento almacenado `sp_student` con `@opt = 5` para eliminar un registro.

- **Parámetros de Ruta:**
  - `id` (int): El ID del estudiante a eliminar.

- **Ejemplo de Petición:**

  ```http
  DELETE /api/student/1
  ```

- **Respuesta Exitosa:**
  ```json
  {
      "success": true,
      "message": "Estudiante eliminado con éxito",
      "data": 1
  }
  ```

- **Errores Potenciales por Validaciones:**
  - `400 Bad Request`: Si no se proporciona un ID válido.
  - `500 Internal Server Error`: Si ocurre un error en la base de datos o un error interno del servidor.

