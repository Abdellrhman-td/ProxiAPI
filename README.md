# 🚀 ProxiWork API

![API](https://img.shields.io/badge/API-REST-blue)
![Status](https://img.shields.io/badge/status-active-success)
![Auth](https://img.shields.io/badge/auth-JWT-orange)
![Content](https://img.shields.io/badge/content-JSON-green)
![License](https://img.shields.io/badge/license-private-lightgrey)

A backend REST API for the **ProxiWork Job Marketplace System**, enabling job seekers and employers to connect through a location-based job platform.

---

## 📌 Base Configuration

| Key | Value |
|-----|------|
| Base URL | `https://localhost:7269` |
| Content-Type | `application/json` |
| Authentication | JWT Bearer Token |

---

## 🔐 Authentication

The API uses **JWT (JSON Web Token)** for secure access.

Include the token in protected requests:

```http
Authorization: Bearer <YOUR_TOKEN>
